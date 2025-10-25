for ($i = 1; $i -le 100; $i++) {

    $currentTime = Get-Date
    Write-Host "=================================================== Running iteration $i at $currentTime ===================================================================="

    & dotnet test C:\src\Metalama.Documentation\code\Metalama.Documentation.Snippets.TestBased.sln

    if ($LASTEXITCODE -ne 0)
    {
        Throw "dotnet test failed."
    }

}

Write-Host "Loop finished."
