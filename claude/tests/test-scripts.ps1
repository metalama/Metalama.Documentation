<#
.SYNOPSIS
Test harness for the skill helper scripts (Python and PowerShell variants).

.DESCRIPTION
Runs the same assertions against scripts/find-api.* and scripts/find-doc.* in
both languages. Compatible with Windows PowerShell 5.1 and PowerShell 7+; run
it under each host to validate both:

    powershell -File claude\tests\test-scripts.ps1 -SkillRoot <path-to-built-skill>
    pwsh -File claude\tests\test-scripts.ps1 -SkillRoot <path-to-built-skill>

The skill root must have the built skill layout: api\.manifest and index.yml.
PowerShell scripts are executed in-process (by the host running this harness);
Python tests are skipped if python is not on PATH.

Exits 0 if all executed tests pass, 1 otherwise.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $SkillRoot
)

$ErrorActionPreference = 'Stop'

$scriptsDir = Join-Path (Split-Path -Parent $PSScriptRoot) 'scripts'

if (-not (Test-Path (Join-Path (Join-Path $SkillRoot 'api') '.manifest'))) {
    Write-Output "SkillRoot '$SkillRoot' does not contain api\.manifest. Point -SkillRoot at a built skill."
    exit 2
}

if (-not (Test-Path (Join-Path $SkillRoot 'index.yml'))) {
    Write-Output "SkillRoot '$SkillRoot' does not contain index.yml. Point -SkillRoot at a built skill."
    exit 2
}

$script:passedCount = 0
$script:failedCount = 0
$script:skippedCount = 0

function Invoke-Tool {
    param(
        [string] $Language,
        [string] $ScriptBaseName,
        [string[]] $Arguments
    )

    Push-Location $SkillRoot

    try {
        if ($Language -eq 'python') {
            $scriptPath = Join-Path $scriptsDir ($ScriptBaseName + '.py')
            $output = & python $scriptPath @Arguments 2>&1 | Out-String
        }
        else {
            $scriptPath = Join-Path $scriptsDir ($ScriptBaseName + '.ps1')
            $output = & $scriptPath @Arguments 2>&1 | Out-String
        }

        $exitCode = $LASTEXITCODE
    }
    finally {
        Pop-Location
    }

    return @{ Output = $output; ExitCode = $exitCode }
}

function Assert-Tool {
    param(
        [string] $Name,
        [string] $Language,
        [string] $ScriptBaseName,
        [string[]] $Arguments,
        [int] $ExpectedExitCode,
        [string[]] $MustContain
    )

    $result = Invoke-Tool -Language $Language -ScriptBaseName $ScriptBaseName -Arguments $Arguments
    $problems = @()

    if ($result.ExitCode -ne $ExpectedExitCode) {
        $problems += "expected exit code $ExpectedExitCode, got $($result.ExitCode)"
    }

    foreach ($fragment in $MustContain) {
        if ($result.Output.IndexOf($fragment, [System.StringComparison]::OrdinalIgnoreCase) -lt 0) {
            $problems += "output does not contain '$fragment'"
        }
    }

    if ($problems.Count -eq 0) {
        Write-Output "  [PASS] $Language $Name"
        $script:passedCount++
    }
    else {
        Write-Output "  [FAIL] $Language $Name -- $($problems -join '; ')"
        $firstLines = ($result.Output -split "`n" | Select-Object -First 5) -join "`n"
        Write-Output ("         output head: " + $firstLines)
        $script:failedCount++
    }
}

function Test-Language {
    param([string] $Language)

    Write-Output "== $Language =="

    Assert-Tool -Name 'find-api: exact UID' -Language $Language -ScriptBaseName 'find-api' `
        -Arguments @('Metalama.Framework.Aspects.OverrideMethodAspect') -ExpectedExitCode 0 `
        -MustContain @('1 match(es)', '- uid: Metalama.Framework.Aspects.OverrideMethodAspect', 'OverrideMethod')

    Assert-Tool -Name 'find-api: substring search' -Language $Language -ScriptBaseName 'find-api' `
        -Arguments @('IntroduceMethod') -ExpectedExitCode 0 `
        -MustContain @('match(es)', 'api/', 'AdviceKind')

    Assert-Tool -Name 'find-api: migration quarantine visible' -Language $Language -ScriptBaseName 'find-api' `
        -Arguments @('OnMethodBoundaryAspect') -ExpectedExitCode 0 `
        -MustContain @('api/migration/PostSharp.')

    Assert-Tool -Name 'find-api: no match' -Language $Language -ScriptBaseName 'find-api' `
        -Arguments @('ZzzDefinitelyNotAnApi') -ExpectedExitCode 1 `
        -MustContain @('No API matching')

    Assert-Tool -Name 'find-doc: keyword search' -Language $Language -ScriptBaseName 'find-doc' `
        -Arguments @('caching', 'invalidation') -ExpectedExitCode 0 `
        -MustContain @('patterns/caching', 'path:')

    Assert-Tool -Name 'find-doc: no match' -Language $Language -ScriptBaseName 'find-doc' `
        -Arguments @('zzzdefinitelynotadoc') -ExpectedExitCode 1 `
        -MustContain @('0 match(es)')
}

Write-Output "Skill helper script tests (host: PowerShell $($PSVersionTable.PSVersion))"
Write-Output "SkillRoot: $SkillRoot"
Write-Output ''

# Python variants: skip if python is missing or is the Windows Store shim.
$pythonWorks = $false
$pythonCommand = Get-Command python -ErrorAction SilentlyContinue

if ($pythonCommand) {
    & python --version *> $null

    if ($LASTEXITCODE -eq 0) {
        $pythonWorks = $true
    }
}

if ($pythonWorks) {
    Test-Language -Language 'python'
}
else {
    Write-Output '== python == (skipped: python not available)'
    $script:skippedCount += 6
}

Test-Language -Language 'powershell'

Write-Output ''
Write-Output "Passed: $script:passedCount  Failed: $script:failedCount  Skipped: $script:skippedCount"

if ($script:failedCount -gt 0) {
    exit 1
}

exit 0
