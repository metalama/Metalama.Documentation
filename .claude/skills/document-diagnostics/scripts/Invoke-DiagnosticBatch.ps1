<#
.SYNOPSIS
    Builds and runs the snapshot tests of a batch of diagnostic IDs in a single build, accepts the output,
    and verifies each ID.

.DESCRIPTION
    Building the test project is the expensive step, so run this once per batch, never once per ID.
    The script holds a machine-wide mutex while building, so concurrent invocations wait instead of
    corrupting each other's obj/ directory.

    Steps:
      1. dotnet test on Metalama.Documentation.SampleCode.Errors, filtered to the batch (DisplayName~LAMA####_).
      2. dotnet msbuild -t:AcceptTestOutput, which copies obj/transformed/**/*.t.cs next to the test files.
      3. Verification of each ID:
           - LAMA####_Error.t.cs reports the target ID (with the severity declared by Metalama).
           - LAMA####_Error.t.cs reports no other error, unless allowed with -AllowExtra.
           - LAMA####_Fixed.t.cs reports neither the target ID nor any error.
           - Both HTML renderings exist under obj/html.
           - content/errors/lama####.md exists and references both samples.
         IDs whose article declares 'diagnostic-sample: none' only get the article check.

    Results are printed as a table and written to artifacts/diagnostics/last-batch.json. The test log is
    written to artifacts/diagnostics/last-batch.log.

.PARAMETER Id
    Diagnostic IDs of the batch, for example LAMA0037,LAMA0101.

.PARAMETER NoBuild
    Skip the build and the tests, and only verify the files already on disk.

.PARAMETER NoAccept
    Do not copy obj/transformed output into the source tree.

.PARAMETER AllowExtra
    Diagnostic IDs that may appear in an _Error snapshot besides the target ID, or as an intended warning in a
    _Fixed snapshot (for example a user diagnostic that the fixed sample reports on purpose).

.EXAMPLE
    ./Invoke-DiagnosticBatch.ps1 -Id LAMA0037,LAMA0101,LAMA0104
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)] [string[]] $Id,
    [switch] $NoBuild,
    [switch] $NoAccept,
    [string[]] $AllowExtra = @()
)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '../../../..')).Path
$projectDir = Join-Path $repoRoot 'code/Metalama.Documentation.SampleCode.Errors'
$project = Join-Path $projectDir 'Metalama.Documentation.SampleCode.Errors.csproj'
$articlesDir = Join-Path $repoRoot 'content/errors'
$outDir = Join-Path $repoRoot 'artifacts/diagnostics'
New-Item -ItemType Directory -Force $outDir | Out-Null

$ids = $Id | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim().ToUpperInvariant() } | Where-Object { $_ } | Sort-Object -Unique
$allowed = $AllowExtra | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim().ToUpperInvariant() }

$inventoryPath = Join-Path $outDir 'inventory.json'
$inventory = if (Test-Path $inventoryPath) { Get-Content $inventoryPath -Raw | ConvertFrom-Json } else { @() }

function Get-FrontMatterValue([string] $path, [string] $key) {
    $content = Get-Content $path -Raw
    if ($content -match "(?s)^﻿?---(.*?)---") {
        $m = [regex]::Match($Matches[1], "(?m)^$key\s*:\s*(.+)$")
        if ($m.Success) { return $m.Groups[1].Value.Trim().Trim('"') }
    }
    return $null
}

$sampleIds = $ids | Where-Object {
    $article = Join-Path $articlesDir "$($_.ToLowerInvariant()).md"
    -not ((Test-Path $article) -and (Get-FrontMatterValue $article 'diagnostic-sample') -eq 'none')
}

$testOutcomes = @{}
$logPath = Join-Path $outDir 'last-batch.log'

if (-not $NoBuild -and $sampleIds) {
    $mutex = [System.Threading.Mutex]::new($false, 'Global\Metalama.Documentation.SampleCode.Errors.Build')
    Write-Host 'Waiting for the build lock...'
    try { [void] $mutex.WaitOne() } catch [System.Threading.AbandonedMutexException] { }

    try {
        $filter = ($sampleIds | ForEach-Object { "DisplayName~$($_)_" }) -join '|'
        $trxName = 'last-batch.trx'
        $resultsDir = Join-Path $outDir 'TestResults'
        Remove-Item (Join-Path $resultsDir $trxName) -ErrorAction SilentlyContinue

        Write-Host "Testing $($sampleIds.Count) IDs: $($sampleIds -join ', ')"
        & dotnet test $project --filter $filter --logger "trx;LogFileName=$trxName" --results-directory $resultsDir *> $logPath
        $testExitCode = $LASTEXITCODE

        # A build failure means no test ran: show the compiler errors and stop.
        # Only MSBuild lines (ending with the project path) count: test output also contains 'error CS' lines from pipelines.
        $buildErrors = Select-String -Path $logPath -Pattern ': error (CS|MSB|NU)\d+.*\[[^\]]+\.csproj\]\s*$' | ForEach-Object { $_.Line.Trim() } | Sort-Object -Unique
        if ($buildErrors) {
            Write-Host 'BUILD FAILED. The test project does not compile (remember: every file is also compiled as plain C#):' -ForegroundColor Red
            $buildErrors | Select-Object -First 30 | ForEach-Object { Write-Host "  $_" }
            exit 2
        }

        $trx = Join-Path $resultsDir $trxName
        if (Test-Path $trx) {
            [xml] $trxXml = Get-Content $trx -Raw
            foreach ($r in $trxXml.TestRun.Results.UnitTestResult) { $testOutcomes[$r.testName] = $r.outcome }
        }
        elseif ($testExitCode -ne 0) {
            Write-Host "dotnet test failed with exit code $testExitCode and produced no results. See $logPath." -ForegroundColor Red
            exit 2
        }

        # Output of samples deleted since an earlier run (for example an ID converted to doc-only) must not be accepted
        # back into the source tree.
        foreach ($stale in (Get-ChildItem (Join-Path $projectDir 'obj/transformed/net10.0-windows') -Directory -Filter 'LAMA*' -ErrorAction SilentlyContinue)) {
            if (-not (Test-Path (Join-Path $projectDir $stale.Name))) { Remove-Item $stale.FullName -Recurse -Force }
        }

        if (-not $NoAccept) {
            & dotnet msbuild $project -t:AcceptTestOutput -nologo -v:q *>> $logPath
        }
    }
    finally {
        $mutex.ReleaseMutex()
        $mutex.Dispose()
    }
}

# Parses the '// Error LAMA0037 on `x`: `message`' lines of a snapshot.
function Get-SnapshotDiagnostics([string] $path) {
    if (-not (Test-Path $path)) { return $null }
    Get-Content $path | ForEach-Object {
        if ($_ -match '^//\s*(Error|Warning|Info|Hidden)\s+([A-Z]+\d+)\b') { [pscustomobject]@{ Severity = $Matches[1]; Id = $Matches[2]; Line = $_ } }
    }
}

$results = foreach ($diagId in $ids) {
    $problems = [System.Collections.Generic.List[string]]::new()
    $lowerId = $diagId.ToLowerInvariant()
    $dir = Join-Path $projectDir $diagId
    $article = Join-Path $articlesDir "$lowerId.md"
    $expectedSeverity = ($inventory | Where-Object Id -EQ $diagId | Select-Object -First 1).Severity
    $docOnly = $sampleIds -notcontains $diagId

    # Message review: the proposal exists, the article records the verdict, and its Message row shows the reviewed text.
    $proposalPath = Join-Path $projectDir "MessageReview/$diagId.json"
    $proposal = if (Test-Path $proposalPath) { Get-Content $proposalPath -Raw | ConvertFrom-Json } else { $null }
    if (-not $proposal) { $problems.Add("missing MessageReview/$diagId.json") }
    elseif ($proposal.verdict -notin 'ok', 'correct') { $problems.Add("proposal verdict must be 'ok' or 'correct'") }
    if ((Test-Path $article) -and $proposal) {
        $review = Get-FrontMatterValue $article 'message-review'
        $expectedReview = if ($proposal.verdict -eq 'correct') { 'corrected' } else { 'ok' }
        if ($review -ne $expectedReview) { $problems.Add("article front matter must have 'message-review: $expectedReview'") }

        $sourceMessage = ($inventory | Where-Object Id -EQ $diagId | Select-Object -First 1).Message
        $expectedMessage = if ($proposal.verdict -eq 'correct' -and $proposal.message) { $proposal.message } else { $sourceMessage }
        # The message is in a code span, with double backticks when the message itself contains a backtick.
        # Several rows are allowed when the ID has several definitions; one of them must match.
        $rows = @([regex]::Matches((Get-Content $article -Raw), '(?m)^\|\s*\*\*Message\*\*\s*\|\s*(`+) ?(.*?) ?\1\s*\|\s*$'))
        # The article shows each {n} placeholder as a meaningful name such as <Method>.
        $messagePattern = if ($expectedMessage) { '^' + ([regex]::Escape($expectedMessage.Trim()) -replace '\\\{\d+(:[^}]*)?}', '<[A-Za-z][A-Za-z0-9 ]*>') + '$' }
        if (-not $rows) { $problems.Add("article has no '| **Message** | ``...`` |' row") }
        elseif ($expectedMessage -and -not ($rows | Where-Object { $_.Groups[2].Value.Trim() -match $messagePattern })) {
            $problems.Add("the article's Message row differs from the $(if ($proposal.verdict -eq 'correct') { 'proposed' } else { 'source' }) message")
        }
    }

    if (-not (Test-Path $article)) { $problems.Add("missing content/errors/$lowerId.md") }
    elseif (-not $docOnly) {
        $text = Get-Content $article -Raw
        foreach ($kind in 'Error', 'Fixed') {
            if ($text -notmatch [regex]::Escape("$diagId/$($diagId)_$kind.cs")) { $problems.Add("article does not reference $($diagId)_$kind.cs") }
        }
    }

    if ($docOnly -and (Test-Path $dir)) { $problems.Add("the article is doc-only, but the folder $diagId/ exists: delete it") }

    if (-not $docOnly) {
        foreach ($kind in 'Error', 'Fixed') {
            $name = "$($diagId)_$kind"
            if (-not (Test-Path (Join-Path $dir "$name.cs"))) { $problems.Add("missing $diagId/$name.cs"); continue }
            if ($testOutcomes.ContainsKey($name) -and $testOutcomes[$name] -ne 'Passed' -and $NoAccept) { $problems.Add("$name test $($testOutcomes[$name])") }

            $snapshot = Join-Path $dir "$name.t.cs"
            $diagnostics = @(Get-SnapshotDiagnostics $snapshot)
            if (-not (Test-Path $snapshot)) { $problems.Add("missing $name.t.cs (test did not run?)"); continue }
            if ((Get-Content $snapshot -Raw) -match 'TODO: Replace this file') { $problems.Add("$name.t.cs is a placeholder (test did not produce output)"); continue }

            if (-not (Test-Path (Join-Path $projectDir "obj/html/net10.0-windows/$diagId/$name.cs.html"))) { $problems.Add("missing HTML for $name") }

            if ($kind -eq 'Error') {
                $target = @($diagnostics | Where-Object Id -EQ $diagId)
                if (-not $target) { $problems.Add("$name does not report $diagId; it reports: " + ((@($diagnostics | ForEach-Object { "$($_.Severity) $($_.Id)" }) -join ', ') -replace '^$', 'nothing')) }
                elseif ($expectedSeverity -and ($target[0].Severity -ne $expectedSeverity)) { $problems.Add("$name reports $diagId as $($target[0].Severity), expected $expectedSeverity") }
                $extra = @($diagnostics | Where-Object { $_.Id -ne $diagId -and $_.Severity -eq 'Error' -and $allowed -notcontains $_.Id })
                if ($extra) { $problems.Add("$name also reports: " + (($extra | ForEach-Object Id | Sort-Object -Unique) -join ', ')) }
            }
            else {
                $bad = @($diagnostics | Where-Object { $_.Id -eq $diagId -or $_.Severity -eq 'Error' })
                if ($bad) { $problems.Add("$name still reports: " + (($bad | ForEach-Object { "$($_.Severity) $($_.Id)" } | Sort-Object -Unique) -join ', ')) }
                $warnings = @($diagnostics | Where-Object { $_.Severity -eq 'Warning' -and $_.Id -ne $diagId -and $allowed -notcontains $_.Id })
                if ($warnings) { $problems.Add("$name has warnings (check they are intended): " + (($warnings | ForEach-Object Id | Sort-Object -Unique) -join ', ')) }
            }
        }
    }

    [pscustomobject]@{
        Id       = $diagId
        Mode     = if ($docOnly) { 'doc-only' } else { 'samples' }
        Result   = if ($problems.Count) { 'FAIL' } else { 'OK' }
        Problems = @($problems)
    }
}

$results | ConvertTo-Json -Depth 3 | Set-Content (Join-Path $outDir 'last-batch.json') -Encoding utf8

foreach ($r in $results) {
    $color = if ($r.Result -eq 'OK') { 'Green' } else { 'Yellow' }
    Write-Host ("{0}  {1,-4}  {2}" -f $r.Id, $r.Result, $r.Mode) -ForegroundColor $color
    foreach ($p in $r.Problems) { Write-Host "            - $p" }
}

$failed = @($results | Where-Object Result -EQ 'FAIL').Count
Write-Host "$($results.Count - $failed)/$($results.Count) OK. Log: artifacts/diagnostics/last-batch.log"
if ($failed) { exit 1 }
