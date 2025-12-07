---
uid: creating-logs
summary: "This article shows how to generate log files for reporting Metalama bugs, including installing the CLI tool, editing diagnostics.json, restarting processes, executing Metalama, and accessing the log file."
keywords: "Metalama bugs, generate log files, install Metalama CLI tool, edit diagnostics.json, restart processes, execute Metalama, access log file, .NET tool, logging configuration, IDE processes"
created-date: 2023-01-11
modified-date: 2025-11-30
---

# Enabling logging

> [!NOTE]
> This procedure is for development machines. For build servers, see <xref:troubleshooting-unattended-build>.

When reporting a Metalama bug, attaching Metalama log files is helpful. This article shows how to generate these logs.

You can produce log files or write the logging output to the console.

## Producing log files

### Step 1. Install the Metalama CLI tool

Install the `metalama` .NET tool as described in <xref:dotnet-tool>.

### Step 2. Edit diagnostics.json

Run the following command:

```powershell
metalama config edit diagnostics
```

This command opens a `diagnostics.json` file in your default editor. Make these changes:

1. In the `logging/processes` section, set the processes that should have logging enabled to `true`:
    * `Compiler`: The compile-time process.
    * `Rider`: The design-time Roslyn process of the Rider IDE.
    * `DevEnv`: The UI process of Visual Studio. No aspect code runs in this process.
    * `RoslynCodeAnalysisService`: The design-time Roslyn process running under Visual Studio, where aspect code runs.
2. In the `logging/trace` section, set the categories that should have logging enabled to `true`. To enable logging for all categories, set the `*` category to `true`.

This example shows how to enable logging for the compiler process for all categories.

```json
{
    "logging": {
        "processes": {
                "Other": false,
                "Compiler": true,
                "DevEnv": false,
                "RoslynCodeAnalysisService": false,
                "Rider": false,
                "BackstageWorker": false,
                "MetalamaConfig": false,
                "TestHost": false
      },
    "trace": {
        "*": false
      },
    "stopLoggingAfterHours": 5.0
  }
}
```

To validate the correctness of the JSON file, run the following command:

```powershell
metalama config validate diagnostics
```

### Step 3. Restart processes

Diagnostic settings are cached in all processes, including background compiler processes and IDE helper processes.

To restart background compiler processes, run the following command:

```powershell
metalama kill
```

To alter the logging configuration of the IDE processes, manually restart your IDE.

### Step 4. Execute Metalama

Perform the actions you want to log.

> [!WARNING]
> Logging is automatically disabled after a certain number of hours following the last modification of `diagnostics.json`. The duration is specified in the `stopLoggingAfterHours` property in the `logging` section and defaults to 2 hours. To change this duration, edit the `diagnostics.json` file.

### Step 5. Open the log file

Find the log in the `%TEMP%\Metalama\Logs` directory.

> [!div class="see-also"]
> <xref:configuration>
> <xref:dotnet-tool>
> <xref:troubleshooting-unattended-build>
> <xref:profiling>
> <xref:process-dump>
