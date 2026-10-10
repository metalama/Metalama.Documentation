<#
.SYNOPSIS
    Applies the reviewed diagnostic message corrections to a dedicated Metalama worktree, and updates the
    Metalama test snapshots that contain the old messages.

.DESCRIPTION
    Writers never edit the Metalama sources: several diagnostics share one descriptor file, so parallel edits
    would conflict. Each writer saves a proposal in
    code/Metalama.Documentation.SampleCode.Errors/MessageReview/LAMA####.json
    (see references/conventions.md). The coordinator reviews the proposals, then runs this script.

    For each proposal with verdict "correct" that is not applied yet, the script:
      1. Replaces the message and title arguments of the DiagnosticDefinition. Literal arguments and string
         constants defined in the same file are supported; interpolated strings are reported for a manual edit.
      2. Rewrites the '// Error LAMA#### on `x`: `message`' lines of the *.t.cs snapshots under the test
         directories, mapping the old placeholders to the new ones.
      3. Lists other test files that still contain a fragment of the old text, for a manual update.
      4. Records the date in the proposal's "applied" property.

    It refuses proposals that introduce a placeholder the old message does not have, because that requires a
    code change at the report sites.

    Safety: the script only edits a worktree whose current branch starts with 'topic/' and that is not the main
    checkout of the repository. Create one with:
        git -C <Metalama> worktree add <Metalama>/.claude/worktrees/diagnostic-messages -b topic/2026.1/<issue>-diagnostic-messages
    Premium diagnostics are only edited with -PremiumRoot pointing to such a worktree of Metalama.Premium.

.PARAMETER MetalamaRoot
    The dedicated Metalama worktree. Defaults to $env:METALAMA_MESSAGES_WORKTREE.

.PARAMETER PremiumRoot
    The dedicated Metalama.Premium worktree, if Premium diagnostics must be corrected.

.PARAMETER Id
    Only apply these proposals. By default, all pending reviewed proposals are applied.

.PARAMETER WhatIf
    Show what would change without writing any file.
#>
[CmdletBinding()]
param(
    [string] $MetalamaRoot,
    [string] $PremiumRoot,
    [string[]] $Id,
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'DiagnosticParsing.ps1')

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../..')).Path
$proposalsDir = Join-Path $repoRoot 'code/Metalama.Documentation.SampleCode.Errors/MessageReview'

if (-not $MetalamaRoot) { $MetalamaRoot = $env:METALAMA_MESSAGES_WORKTREE }
if (-not $MetalamaRoot) { throw 'Pass -MetalamaRoot (a dedicated Metalama worktree) or set $env:METALAMA_MESSAGES_WORKTREE.' }

# Refuses anything but a linked worktree on a topic branch.
function Assert-DedicatedWorktree([string] $root) {
    $root = (Resolve-Path $root).Path
    $branch = (& git -C $root branch --show-current).Trim()
    if (-not $branch.StartsWith('topic/')) { throw "$root is on '$branch'. Use a dedicated worktree on a topic/ branch." }
    # In a linked worktree, the worktree's git directory differs from the repository's common git directory.
    $dirs = @(& git -C $root rev-parse --path-format=absolute --git-dir --git-common-dir)
    if ([IO.Path]::GetFullPath($dirs[0]) -eq [IO.Path]::GetFullPath($dirs[1])) {
        throw "$root is the main checkout of its repository. Use a dedicated worktree (git worktree add)."
    }
    return $root
}

$roots = @(Assert-DedicatedWorktree $MetalamaRoot)
if ($PremiumRoot) { $roots += Assert-DedicatedWorktree $PremiumRoot }
$baseDir = Split-Path $roots[0]

$sourceDirs = $roots | ForEach-Object {
    Join-Path $_ 'Metalama.Framework/src'; Join-Path $_ 'Metalama.Extensions/src'; Join-Path $_ 'Metalama.Patterns/src'; Join-Path $_ 'src'
} | Where-Object { Test-Path $_ } | Sort-Object -Unique

$allCs = $sourceDirs | ForEach-Object { Get-ChildItem $_ -Recurse -Filter *.cs -File } |
    Where-Object { $_.FullName -notmatch '[\\/](obj|bin)[\\/]' } | Sort-Object FullName -Unique
$sourceFiles = $allCs | Where-Object { $_.FullName -notmatch '[\\/]tests?[\\/]' -and $_.Name -notmatch '\.(t|g)\.cs$' }
$snapshotFiles = $allCs | Where-Object { $_.Name -like '*.t.cs' }
$testCodeFiles = $allCs | Where-Object { $_.FullName -match '[\\/]tests?[\\/]' -and $_.Name -notlike '*.t.cs' }

function Get-RelativePath([string] $path) { [IO.Path]::GetRelativePath($baseDir, $path) }

# Writes a file with the same byte-order mark as before (Get-Content -Raw and ReadAllText drop it).
function Write-PreservingEncoding([string] $path, [string] $content) {
    $bytes = [IO.File]::ReadAllBytes($path)
    $hasBom = $bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF
    [IO.File]::WriteAllText($path, $content.TrimStart([char] 0xFEFF), [Text.UTF8Encoding]::new($hasBom))
}

# Formats a C# string literal, split into several concatenated literals when it is long.
function Format-CSharpLiteral([string] $value, [string] $indent, [string] $eol) {
    $escaped = $value.Replace('\', '\\').Replace('"', '\"')
    $chunks = [System.Collections.Generic.List[string]]::new()
    while ($escaped.Length -gt 130) {
        $cut = $escaped.LastIndexOf(' ', 130)
        if ($cut -le 0) { break }
        $chunks.Add($escaped.Substring(0, $cut + 1))
        $escaped = $escaped.Substring($cut + 1)
    }
    $chunks.Add($escaped)
    return ($chunks | ForEach-Object { '"' + $_ + '"' }) -join " +$eol$indent"
}

function Get-Placeholders([string] $format) {
    [regex]::Matches($format, '\{(\d+)(?:[,:][^}]*)?\}') | ForEach-Object { [int] $_.Groups[1].Value } | Sort-Object -Unique
}

# Converts a message format into a regex that captures each placeholder value.
function ConvertTo-MessageRegex([string] $format) {
    $seen = @{}
    $pattern = foreach ($part in [regex]::Split($format, '(\{\d+(?:[,:][^}]*)?\})')) {
        if ($part -match '^\{(\d+)') {
            $n = $Matches[1]
            if ($seen[$n]) { "\k<p$n>" } else { $seen[$n] = $true; "(?<p$n>.*?)" }
        }
        else { [regex]::Escape($part) }
    }
    return '^' + ($pattern -join '') + '$'
}

function ConvertTo-Replacement([string] $format) {
    return [regex]::Replace($format.Replace('$', '$$'), '\{(\d+)(?:[,:][^}]*)?\}', '${p$1}')
}

$proposalFiles = @(if (Test-Path $proposalsDir) { Get-ChildItem $proposalsDir -Filter 'LAMA*.json' })
if ($Id) {
    $ids = $Id | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim().ToUpperInvariant() }
    $proposalFiles = $proposalFiles | Where-Object { $ids -contains $_.BaseName }
}

$summary = [System.Collections.Generic.List[object]]::new()

foreach ($pf in $proposalFiles) {
    $p = Get-Content $pf.FullName -Raw | ConvertFrom-Json
    if ($p.verdict -ne 'correct' -or $p.applied) { continue }
    $problems = [System.Collections.Generic.List[string]]::new()
    $notes = [System.Collections.Generic.List[string]]::new()

    # Find the definition.
    $candidates = @(foreach ($f in ($sourceFiles | Where-Object { Select-String -Path $_.FullName -Pattern "`"$($p.id)`"" -SimpleMatch -Quiet })) {
            $text = Get-Content $f.FullName -Raw
            foreach ($d in Get-DiagnosticDefinitions $text (Get-DefaultCategory $text)) {
                if ($d.IsDefinition -and $d.Id -eq $p.id -and (-not $p.symbol -or $d.Symbol -eq $p.symbol)) {
                    [pscustomobject]@{ File = $f.FullName; Definition = $d }
                }
            }
        })
    if ($candidates.Count -ne 1) {
        $summary.Add([pscustomobject]@{ Id = $p.id; Result = 'SKIPPED'; Details = @("found $($candidates.Count) definitions in the given worktrees; set 'symbol' in the proposal or pass -PremiumRoot") })
        continue
    }
    $file = $candidates[0].File
    $def = $candidates[0].Definition
    $text = Get-Content $file -Raw

    # Plan the replacements, then apply them from the end of the file so that offsets stay valid.
    $edits = [System.Collections.Generic.List[object]]::new()
    foreach ($kind in 'message', 'title') {
        $newValue = $p.$kind
        if (-not $newValue) { continue }
        $arg = if ($kind -eq 'message') { $def.MessageArg } else { $def.TitleArg }
        $oldValue = if ($kind -eq 'message') { $def.Message } else { $def.Title }
        if (-not $arg) { $problems.Add("the definition has no $kind argument: apply by hand"); continue }
        if ($newValue -ceq $oldValue) { continue }

        $oldPlaceholders = @(Get-Placeholders $oldValue)
        $added = @(Get-Placeholders $newValue | Where-Object { $oldPlaceholders -notcontains $_ })
        if ($added) { $problems.Add("the new $kind uses placeholders {$($added -join '}, {')} that the old one does not have: apply by hand with the code change"); continue }

        $start = $arg.Start
        $length = $arg.Length
        if ($arg.Kind -eq 'Constant') {
            $const = [regex]::Match($text, "(const\s+string\s+$($arg.ConstName)\s*=\s*)([^;]+);")
            if (-not $const.Success) { $problems.Add("cannot find the constant $($arg.ConstName)"); continue }
            $start = $const.Groups[2].Index
            $length = $const.Groups[2].Length
        }
        elseif ($arg.Kind -ne 'Literal') { $problems.Add("the $kind is an $($arg.Kind.ToLowerInvariant()) expression: apply by hand"); continue }

        # Continuation lines get the indentation of the line where the argument starts.
        $lineStart = $text.LastIndexOf("`n", [Math]::Max(0, $start - 1)) + 1
        $indent = [regex]::Match($text.Substring($lineStart, $start - $lineStart), '^[ \t]*').Value
        $edits.Add([pscustomobject]@{ Kind = $kind; Start = $start; Length = $length; Value = (Format-CSharpLiteral $newValue $indent $(if ($text.Contains("`r`n")) { "`r`n" } else { "`n" })); Old = $oldValue; New = $newValue })
    }

    if ($problems.Count) { $summary.Add([pscustomobject]@{ Id = $p.id; Result = 'SKIPPED'; Details = @($problems) }); continue }

    foreach ($e in ($edits | Sort-Object Start -Descending)) { $text = $text.Remove($e.Start, $e.Length).Insert($e.Start, $e.Value) }
    if (-not $WhatIf -and $edits.Count) { Write-PreservingEncoding $file $text }
    $notes.Add("source: $(Get-RelativePath $file) ($(($edits | ForEach-Object Kind) -join ', '))")

    # Update the snapshots.
    $messageEdit = $edits | Where-Object Kind -EQ 'message'
    if ($messageEdit) {
        $messageRegex = [regex]::new((ConvertTo-MessageRegex $messageEdit.Old))
        $replacement = ConvertTo-Replacement $messageEdit.New
        $lineRegex = [regex]::new("^(//\s*(?:Error|Warning|Info|Hidden)\s+$($p.id)\b.*?``:\s*``)(.*)(``\s*)$")
        $updated = 0
        $unmatched = [System.Collections.Generic.List[string]]::new()
        foreach ($s in ($snapshotFiles | Where-Object { Select-String -Path $_.FullName -Pattern $p.id -SimpleMatch -Quiet })) {
            $original = [IO.File]::ReadAllText($s.FullName)
            $eol = if ($original.Contains("`r`n")) { "`r`n" } else { "`n" }
            $lines = $original -split '\r?\n'
            $changed = $false
            for ($i = 0; $i -lt $lines.Length; $i++) {
                $lm = $lineRegex.Match($lines[$i])
                if (-not $lm.Success) { continue }
                if (-not $messageRegex.IsMatch($lm.Groups[2].Value)) { $unmatched.Add("$(Get-RelativePath $s.FullName):$($i + 1)"); continue }
                $lines[$i] = $lm.Groups[1].Value + $messageRegex.Replace($lm.Groups[2].Value, $replacement) + $lm.Groups[3].Value
                $changed = $true
                $updated++
            }
            if ($changed -and -not $WhatIf) { Write-PreservingEncoding $s.FullName ($lines -join $eol) }
        }
        $notes.Add("snapshot lines updated: $updated")
        foreach ($u in $unmatched) { $notes.Add("MANUAL: snapshot line does not match the old format: $u") }

        # Other test code that may assert on the old text: search the clauses of the old text that the new text removes.
        $fragments = [regex]::Split($messageEdit.Old, '\{\d+[^}]*\}|[,.;:()]') | ForEach-Object { $_.Trim() } |
            Where-Object { $_.Length -ge 20 -and -not $messageEdit.New.Contains($_) } | Sort-Object -Unique
        if ($fragments) {
            foreach ($t in ($testCodeFiles | Where-Object { $f = $_.FullName; $fragments | Where-Object { Select-String -Path $f -Pattern $_ -SimpleMatch -Quiet } })) {
                $notes.Add("MANUAL: test code contains the old text: $(Get-RelativePath $t.FullName)")
            }
        }
    }

    if (-not $WhatIf) {
        $p | Add-Member -Force applied (Get-Date -Format 'yyyy-MM-dd')
        $p | ConvertTo-Json -Depth 4 | Set-Content $pf.FullName -Encoding utf8NoBOM
    }
    $summary.Add([pscustomobject]@{ Id = $p.id; Result = $(if ($WhatIf) { 'WOULD APPLY' } else { 'APPLIED' }); Details = @($notes) })
}

foreach ($s in $summary) {
    Write-Host "$($s.Id)  $($s.Result)" -ForegroundColor $(if ($s.Result -eq 'SKIPPED') { 'Yellow' } else { 'Green' })
    foreach ($d in $s.Details) { Write-Host "    - $d" }
}
if (-not $summary.Count) { Write-Host 'No pending corrections.' }
