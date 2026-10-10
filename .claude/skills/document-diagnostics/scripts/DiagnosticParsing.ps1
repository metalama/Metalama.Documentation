# Shared parser of LAMA diagnostic definitions in C# source files. Dot-sourced by Get-DiagnosticInventory.ps1
# and Set-DiagnosticMessage.ps1. It is a heuristic parser for the few constructor shapes used by Metalama, not a
# C# parser.

# Returns the index of the parenthesis closing the one at $openIndex, skipping comments, string and char literals.
function Find-ClosingParen([string] $text, [int] $openIndex) {
    $depth = 0
    $i = $openIndex
    while ($i -lt $text.Length) {
        $c = $text[$i]
        if ($c -eq '"') { $i = Skip-StringLiteral $text $i }
        elseif ($c -eq "'") {
            $i++
            while ($i -lt $text.Length -and $text[$i] -ne "'") { if ($text[$i] -eq '\') { $i++ }; $i++ }
        }
        elseif ($c -eq '/' -and $i + 1 -lt $text.Length -and $text[$i + 1] -eq '/') {
            while ($i -lt $text.Length -and $text[$i] -ne "`n") { $i++ }
        }
        elseif ($c -eq '(') { $depth++ }
        elseif ($c -eq ')') { $depth--; if ($depth -eq 0) { return $i } }
        $i++
    }
    return -1
}

# Given the index of an opening quote, returns the index of the closing quote.
function Skip-StringLiteral([string] $text, [int] $i) {
    $verbatim = ($i -gt 0 -and $text[$i - 1] -eq '@') -or ($i -gt 1 -and $text[$i - 1] -eq '$' -and $text[$i - 2] -eq '@')
    $i++
    while ($i -lt $text.Length) {
        if ($verbatim -and $text[$i] -eq '"' -and $i + 1 -lt $text.Length -and $text[$i + 1] -eq '"') { $i += 2; continue }
        if (-not $verbatim -and $text[$i] -eq '\') { $i += 2; continue }
        if ($text[$i] -eq '"') { return $i }
        $i++
    }
    return $i
}

# Splits the arguments between $openIndex and $closeIndex at top-level commas. Returns, for each argument, its
# position in $text (without surrounding whitespace and comments) and its text without comments.
function Split-Arguments([string] $text, [int] $openIndex, [int] $closeIndex) {
    $result = [System.Collections.Generic.List[object]]::new()
    $depth = 0
    $start = $openIndex + 1
    $i = $start
    $emit = {
        param($from, $to)
        $raw = $text.Substring($from, $to - $from)
        $clean = [regex]::Replace($raw, '(?m)^\s*//[^\n]*\n?', '').Trim()
        $lead = [regex]::Match($raw, '^(?:\s|//[^\n]*\n)*').Length
        $trimmedLength = $raw.TrimEnd().Length - $lead
        $result.Add([pscustomobject]@{ Start = $from + $lead; Length = [Math]::Max(0, $trimmedLength); Text = $clean })
    }
    while ($i -lt $closeIndex) {
        $c = $text[$i]
        if ($c -eq '"') { $i = Skip-StringLiteral $text $i }
        elseif ($c -eq '/' -and $text[$i + 1] -eq '/') { while ($i -lt $closeIndex -and $text[$i] -ne "`n") { $i++ } }
        elseif ($c -in '(', '<', '[', '{') { $depth++ }
        elseif ($c -in ')', '>', ']', '}') { $depth-- }
        elseif ($c -eq ',' -and $depth -eq 0) { & $emit $start $i; $start = $i + 1 }
        $i++
    }
    & $emit $start $closeIndex
    return $result
}

# Concatenates the string literals of an expression like "a" + "b". Returns $null if there is no literal.
# In interpolated literals, renders {nameof(A.B)} as B and doubled braces as single braces, as C# does at run time.
function Get-StringValue([string] $expr) {
    # Tokens in order: string literals, and nameof(...) expressions concatenated between literals.
    $literals = [regex]::Matches($expr, '(\$?)@?"((?:[^"\\]|\\.|"")*)"|\bnameof\(\s*(?:\w+\.)*(\w+)\s*\)')
    if (-not ($literals | Where-Object { $_.Groups[2].Success })) { return $null }
    $parts = foreach ($l in $literals) {
        if ($l.Groups[3].Success) { $l.Groups[3].Value; continue }
        $value = $l.Groups[2].Value
        if ($l.Groups[1].Value -eq '$') {
            $value = [regex]::Replace($value, '\{nameof\(\s*(?:[\w]+\.)*(\w+)\s*\)\}', '$1').Replace('{{', '{').Replace('}}', '}')
        }
        $value
    }
    return ($parts -join '') -replace '\\"', '"' -replace '\\n', ' ' -replace '\\\\', '\'
}

# Returns the LAMA diagnostic definitions and other LAMA ID usages of a C# source text.
# Each definition has Id, Symbol, Severity, Title, Message, Category, and MessageArg / TitleArg describing the
# argument that holds the text: Start, Length, Kind (Literal, Interpolated, Constant, Other) and ConstName.
function Get-DiagnosticDefinitions([string] $text, [string] $defaultCategory) {
    foreach ($m in [regex]::Matches($text, '"(LAMA\d{4})"')) {
        $diagId = $m.Groups[1].Value
        $before = $text.Substring(0, $m.Index)

        # Is this ID the first argument of a constructor call? (Comments may sit between the parenthesis and the ID.)
        $ctor = [regex]::Match($before, '\bnew\s*(?:[\w\.]+\s*(?:<(?:[^<>]|<[^<>]*>)*>)?)?\s*\((?:\s|//[^\n]*\n)*$')
        if (-not $ctor.Success) {
            [pscustomobject]@{ Id = $diagId; IsDefinition = $false }
            continue
        }

        $openIndex = $ctor.Index + $ctor.Value.LastIndexOf('(')
        $closeIndex = Find-ClosingParen $text $openIndex
        $arguments = @(Split-Arguments $text $openIndex $closeIndex)

        # The constructors take (id, severity, message, title, category), (id, title, message, category, severity)
        # or (id, category, message, severity, title). The message comes first when the severity or the category
        # precedes the first text argument.
        $severity = $null
        $category = $null
        $texts = [System.Collections.Generic.List[object]]::new()
        $messageFirst = $false
        for ($k = 1; $k -lt $arguments.Count; $k++) {
            $arg = $arguments[$k]
            $a = $arg.Text -replace '^\w+\s*:\s*', ''
            if ($a -match '^(?:(?:Metalama\.Framework\.Diagnostics\.)?Severity\.|DiagnosticSeverity\.)?(Error|Warning|Info|Hidden)$') {
                $severity = $Matches[1]
                if ($texts.Count -eq 0) { $messageFirst = $true }
            }
            elseif ($a -match '^_?\w*[Cc]ategory$' -or $a -match '^"Metalama(\.\w+)+"$') {
                $category = if ($a.StartsWith('"')) { $a.Trim('"') } else { $defaultCategory }
                if ($texts.Count -eq 0) { $messageFirst = $true }
            }
            elseif ($null -ne ($s = Get-StringValue $a)) {
                $kind = if ($a -match '(^|\+\s*)\$') { 'Interpolated' } elseif ($a -match '^("([^"\\]|\\.)*"\s*\+?\s*)+$') { 'Literal' } else { 'Other' }
                $texts.Add([pscustomobject]@{ Value = $s; Start = $arg.Start; Length = $arg.Length; Kind = $kind; ConstName = $null })
            }
            elseif ($a -match '^\w+$') {
                # A named constant defined in the same file.
                $const = [regex]::Match($text, "const\s+string\s+$a\s*=\s*([^;]+);")
                $value = if ($const.Success) { Get-StringValue $const.Groups[1].Value } else { "<$a>" }
                $texts.Add([pscustomobject]@{ Value = $value; Start = $arg.Start; Length = $arg.Length; Kind = 'Constant'; ConstName = $a })
            }
        }

        $messageArg = if ($messageFirst) { $texts[0] } elseif ($texts.Count -gt 1) { $texts[1] } else { $null }
        $titleArg = if ($messageFirst) { if ($texts.Count -gt 1) { $texts[1] } else { $null } } else { $texts[0] }

        $symbol = [regex]::Match($text.Substring(0, $ctor.Index), '(\w+)\s*(?:\{\s*get;\s*\}\s*)?=\s*$')
        [pscustomobject]@{
            Id           = $diagId
            IsDefinition = $true
            Symbol       = if ($symbol.Success) { $symbol.Groups[1].Value } else { $null }
            Severity     = $severity
            Title        = $titleArg.Value
            Message      = $messageArg.Value
            Category     = if ($category) { $category } else { $defaultCategory }
            MessageArg   = $messageArg
            TitleArg     = $titleArg
        }
    }
}

# Returns the value of the '_category' constant of a file, if any.
function Get-DefaultCategory([string] $text) {
    $m = [regex]::Match($text, '_(?:diagnostic)?[cC]ategory\s*=\s*"([^"]+)"')
    if ($m.Success) { return $m.Groups[1].Value }
    return $null
}
