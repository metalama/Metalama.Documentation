<#
.SYNOPSIS
    Summarizes the message proposals of the writers as a Markdown report for review.

.DESCRIPTION
    Reads code/Metalama.Documentation.SampleCode.Errors/MessageReview/LAMA####.json and artifacts/diagnostics/inventory.json, and
    writes artifacts/diagnostics/message-review.md with:
      - the proposed corrections (current and proposed message and title, rationale, required code change);
      - the Metalama bugs found while researching;
      - the IDs whose verdict is 'ok'.
    Read-only: it does not modify any repository.

.PARAMETER Id
    Only include these IDs.
#>
[CmdletBinding()]
param([string[]] $Id)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../..')).Path
$outDir = Join-Path $repoRoot 'artifacts/diagnostics'
$inventory = Get-Content (Join-Path $outDir 'inventory.json') -Raw | ConvertFrom-Json
$proposals = Get-ChildItem (Join-Path $repoRoot 'code/Metalama.Documentation.SampleCode.Errors/MessageReview') -Filter 'LAMA*.json' -ErrorAction SilentlyContinue |
    ForEach-Object { Get-Content $_.FullName -Raw | ConvertFrom-Json } | Sort-Object id
if ($Id) {
    $ids = $Id | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim().ToUpperInvariant() }
    $proposals = $proposals | Where-Object { $ids -contains $_.id }
}

$corrections = @($proposals | Where-Object verdict -EQ 'correct')
$bugs = @($proposals | Where-Object engineBug)
$ok = @($proposals | Where-Object verdict -EQ 'ok')

$sb = [System.Text.StringBuilder]::new()
[void] $sb.AppendLine('# Diagnostic message review')
[void] $sb.AppendLine()
[void] $sb.AppendLine("$($proposals.Count) diagnostics reviewed: $($corrections.Count) corrections proposed, $($ok.Count) messages correct, $($bugs.Count) Metalama bugs found.")
[void] $sb.AppendLine()
[void] $sb.AppendLine('## Proposed corrections')
foreach ($p in $corrections) {
    $current = $inventory | Where-Object Id -EQ $p.id | Select-Object -First 1
    [void] $sb.AppendLine()
    [void] $sb.AppendLine("### $($p.id) ($($current.Symbol), $($current.File))$(if ($p.applied) { " - applied $($p.applied)" })")
    [void] $sb.AppendLine()
    if ($p.message) {
        [void] $sb.AppendLine("* Current message: ``$($current.Message)``")
        [void] $sb.AppendLine("* Proposed message: ``$($p.message)``")
    }
    if ($p.title) {
        [void] $sb.AppendLine("* Current title: ``$($current.Title)``")
        [void] $sb.AppendLine("* Proposed title: ``$($p.title)``")
    }
    [void] $sb.AppendLine("* Rationale: $($p.rationale)")
    if ($p.codeChange) { [void] $sb.AppendLine("* Required code change: $($p.codeChange)") }
}
$codeOnly = @($ok | Where-Object codeChange)
if ($codeOnly) {
    [void] $sb.AppendLine()
    [void] $sb.AppendLine('## Code changes suggested for correct messages')
    [void] $sb.AppendLine()
    foreach ($p in $codeOnly) { [void] $sb.AppendLine("* **$($p.id)**: $($p.codeChange)") }
}
[void] $sb.AppendLine()
[void] $sb.AppendLine('## Metalama bugs found')
[void] $sb.AppendLine()
if ($bugs) { foreach ($p in $bugs) { [void] $sb.AppendLine("* **$($p.id)**: $($p.engineBug)") } } else { [void] $sb.AppendLine('None.') }
[void] $sb.AppendLine()
[void] $sb.AppendLine('## Messages reviewed as correct')
[void] $sb.AppendLine()
[void] $sb.AppendLine($(if ($ok) { ($ok.id -join ', ') } else { 'None.' }))

$path = Join-Path $outDir 'message-review.md'
Set-Content $path $sb.ToString() -Encoding utf8NoBOM
Write-Host "$($proposals.Count) proposals: $($corrections.Count) corrections, $($bugs.Count) bugs. Report: artifacts/diagnostics/message-review.md"
