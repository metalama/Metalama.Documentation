<#
.SYNOPSIS
    Lists every LAMA diagnostic defined in the Metalama sources and its documentation status.

.DESCRIPTION
    Parses DiagnosticDefinition / DiagnosticDescriptor declarations in the Metalama and Metalama.Premium
    source trees and cross-references them with this repository:

      - content/errors/lama####.md                                  (the article)
      - code/Metalama.Documentation.SampleCode.Errors/LAMA####/      (the snapshot tests)

    Status values:
      todo      No article yet.
      partial   Article exists, but it neither has both samples nor declares 'diagnostic-sample: none'.
      done      Article plus LAMA####_Error.cs and LAMA####_Fixed.cs.
      doc-only  Article declares 'diagnostic-sample: none' in its front matter (not reproducible in a snapshot test).
      excluded  Never documented (internal analyzers used to develop Metalama itself).

    The full inventory is also written to artifacts/diagnostics/inventory.json.

.PARAMETER MetalamaRoot
    Root of the Metalama repository. Defaults to $env:METALAMA_SOURCE_ROOT, then ../Metalama next to this repository.

.PARAMETER PremiumRoot
    Root of the Metalama.Premium repository. Defaults to $env:METALAMA_PREMIUM_SOURCE_ROOT, then a sibling of MetalamaRoot.

.PARAMETER Status
    Only list diagnostics with this status.

.PARAMETER Id
    Only list these IDs (for example LAMA0037,LAMA0101). Prints full details.

.PARAMETER Category
    Only list diagnostics whose category matches this wildcard (for example 'Metalama.Template', '*Caching*').

.PARAMETER First
    Only list the first N matching diagnostics (after sorting by ID).

.EXAMPLE
    ./Get-DiagnosticInventory.ps1 -MetalamaRoot X:\src\Metalama-2026.1\Metalama -Status todo -First 12
#>
[CmdletBinding()]
param(
    [string] $MetalamaRoot,
    [string] $PremiumRoot,
    [ValidateSet('todo', 'partial', 'done', 'doc-only', 'excluded')]
    [string] $Status,
    [string[]] $Id,
    [string] $Category,
    [int] $First = 0,
    [switch] $Json
)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../..')).Path
$samplesDir = Join-Path $repoRoot 'code/Metalama.Documentation.SampleCode.Errors'
$articlesDir = Join-Path $repoRoot 'content/errors'

function Resolve-SourceRoot([string] $explicit, [string] $envVar, [string[]] $candidates, [string] $name) {
    if ($explicit) { return (Resolve-Path $explicit).Path }
    $fromEnv = [Environment]::GetEnvironmentVariable($envVar)
    if ($fromEnv) { return (Resolve-Path $fromEnv).Path }
    foreach ($c in $candidates) { if ($c -and (Test-Path $c)) { return (Resolve-Path $c).Path } }
    throw "Cannot find the $name source tree. Pass -$($name -replace '\.', '')Root or set `$env:$envVar."
}

$MetalamaRoot = Resolve-SourceRoot $MetalamaRoot 'METALAMA_SOURCE_ROOT' @((Join-Path $repoRoot '../Metalama')) 'Metalama'
$PremiumRoot = Resolve-SourceRoot $PremiumRoot 'METALAMA_PREMIUM_SOURCE_ROOT' @((Join-Path $MetalamaRoot '../Metalama.Premium')) 'Metalama.Premium'

# Source files that only contain analyzers used to develop Metalama itself.
$excludedPathPatterns = @('*\Metalama.Framework.Engine.Analyzers\*')

$sourceDirs = @(
    (Join-Path $MetalamaRoot 'Metalama.Framework/src'),
    (Join-Path $MetalamaRoot 'Metalama.Extensions/src'),
    (Join-Path $MetalamaRoot 'Metalama.Patterns/src'),
    (Join-Path $PremiumRoot 'src')
) | Where-Object { Test-Path $_ }

$files = $sourceDirs | ForEach-Object {
    Get-ChildItem $_ -Recurse -Filter *.cs -File |
        Where-Object { $_.FullName -notmatch '[\\/](obj|bin|tests?)[\\/]' -and $_.Name -notmatch '\.(t|g)\.cs$' }
} | Where-Object { Select-String -Path $_.FullName -Pattern '"LAMA\d{4}"' -Quiet }


. (Join-Path $PSScriptRoot 'DiagnosticParsing.ps1')

$entries = [System.Collections.Generic.List[object]]::new()

foreach ($file in $files) {
    $text = Get-Content $file.FullName -Raw
    $relative = [IO.Path]::GetRelativePath((Split-Path $MetalamaRoot), $file.FullName)
    $excluded = [bool]($excludedPathPatterns | Where-Object { $file.FullName -like $_ })

    foreach ($d in Get-DiagnosticDefinitions $text (Get-DefaultCategory $text)) {
        $d | Add-Member File $relative
        $d | Add-Member Excluded $excluded
        $entries.Add($d)
    }
}

function Get-FrontMatterValue([string] $path, [string] $key) {
    $content = Get-Content $path -Raw
    if ($content -match "(?s)^\uFEFF?---(.*?)---") {
        $m = [regex]::Match($Matches[1], "(?m)^$key\s*:\s*(.+)$")
        if ($m.Success) { return $m.Groups[1].Value.Trim().Trim('"') }
    }
    return $null
}

$categories = (Import-PowerShellDataFile (Join-Path $PSScriptRoot "Categories.psd1")).Categories
function Get-DocCategory($d) {
    foreach ($c in $categories) {
        if ($d.Category -and $c.SourceCategories -contains $d.Category) { return $c.Key }
        if (-not $d.Category -and ($c.FilePatterns | Where-Object { "\\" + $d.File -like $_ })) { return $c.Key }
    }
    return $null
}

$inventory = $entries | Group-Object Id | ForEach-Object {
    $definitions = @($_.Group | Where-Object IsDefinition)
    $d = if ($definitions.Count) { $definitions[0] } else { $_.Group[0] }
    $lowerId = $d.Id.ToLowerInvariant()
    $article = Join-Path $articlesDir "$lowerId.md"
    $hasError = Test-Path (Join-Path $samplesDir "$($d.Id)/$($d.Id)_Error.cs")
    $hasFixed = Test-Path (Join-Path $samplesDir "$($d.Id)/$($d.Id)_Fixed.cs")
    $sampleMode = if (Test-Path $article) { Get-FrontMatterValue $article 'diagnostic-sample' } else { $null }
    $messageReview = if (Test-Path $article) { Get-FrontMatterValue $article 'message-review' } else { $null }
    $messageReviewed = $messageReview -in 'ok', 'corrected'

    $state = if ($d.Excluded) { 'excluded' }
    elseif (-not (Test-Path $article)) { 'todo' }
    elseif (-not $messageReviewed) { 'partial' }
    elseif ($sampleMode -eq 'none') { 'doc-only' }
    elseif ($hasError -and $hasFixed) { 'done' }
    else { 'partial' }

    [pscustomobject]@{
        Id            = $d.Id
        Status        = $state
        Severity      = $d.Severity
        Category      = $d.Category
        DocCategory   = Get-DocCategory $d
        MessageReview = $messageReview
        Symbol        = $d.Symbol
        Title         = $d.Title
        Message       = $d.Message
        File          = $d.File
        Definitions   = $definitions.Count
        OtherUsages   = @($_.Group | Where-Object { -not $_.IsDefinition } | ForEach-Object File | Sort-Object -Unique)
    }
} | Sort-Object Id

$outDir = Join-Path $repoRoot 'artifacts/diagnostics'
New-Item -ItemType Directory -Force $outDir | Out-Null
$inventory | ConvertTo-Json -Depth 4 | Set-Content (Join-Path $outDir 'inventory.json') -Encoding utf8

$selected = $inventory
if ($Status) { $selected = $selected | Where-Object Status -EQ $Status }
if ($Id) { $ids = $Id | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim().ToUpperInvariant() }; $selected = $selected | Where-Object { $ids -contains $_.Id } }
if ($Category) { $selected = $selected | Where-Object { $_.Category -like $Category } }
if ($First -gt 0) { $selected = $selected | Select-Object -First $First }

if ($Json) { $selected | ConvertTo-Json -Depth 4; return }

if ($Id) {
    $selected | Format-List Id, Status, Severity, Category, DocCategory, MessageReview, Symbol, Title, Message, File, OtherUsages
}
else {
    $selected | Format-Table Id, Status, Severity, DocCategory, Symbol, Title -AutoSize -Wrap
    $summary = ($inventory | Group-Object Status | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ', '
    Write-Host "Total $($inventory.Count): $summary. Full inventory: artifacts/diagnostics/inventory.json"
}
