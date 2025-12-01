---
uid: creating-logs
summary: "The document provides instructions on how to generate log files for reporting Metalama bugs, including installing the CLI tool, editing diagnostics.json, restarting processes, executing Metalama, and accessing the log file."
keywords: "Metalama bugs, generate log files, install Metalama CLI tool, edit diagnostics.json, restart processes, execute Metalama, access log file, .NET tool, logging configuration, IDE processes"
created-date: 2023-01-11
modified-date: 2025-11-30
---

# Enabling logging

> [!NOTE]
> This procedure is suitable for development machines. For build servers, see <xref:troubleshooting-unattended-build>.

When reporting a Metalama bug, it's often helpful to attach Metalama log files. This document provides instructions on how to generate these logs.

There are two possible approaches: produce log files, or write the logging output to the console.

## Producing log files

### Step 1. Install the Metalama CLI tool

First, install the `metalama` .NET tool as outlined in <xref:dotnet-tool>.

### Step 2. Edit diagnostics.json

Run the following command:

```
metalama config edit diagnostics
```

This command will open a `diagnostics.json` file in your default editor. Make the following changes to this file:

1. In the `logging/processes` section, set the processes that should have logging enabled to `true`:
    * `Compiler`: This refers to the compile-time process.
    * `Rider`: This is the design-time Roslyn process of the Rider IDE.
    * `DevEnv`: This is the UI process of Visual Studio. Note that no aspect code runs in this process.
    * `RoslynCodeAnalysisService`: This is the design-time Roslyn process running under Visual Studio, where the aspect code runs.
2. In the `logging/trace` section, set the categories that should have logging enabled to `true`. To enable logging for all categories, set the `*` category to `true`.

The following example shows how to enable logging for the compiler process for all categories.

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

If you need to alter the logging configuration of the IDE processes, you'll need to manually restart your IDE.

### Step 4. Execute Metalama

Perform the sequence of actions that you wish to log.

> [!WARNING]
> Logging is automatically disabled after a certain number of hours following the last modification of `diagnostics.json`. The duration is specified in the `stopLoggingAfterHours` property in the `logging` section and defaults to 2 hours. To change this duration, you can edit the `diagnostics.json` file.

### Step 5. Open the log file

You can find the log in the `%TEMP%\Metalama\Logs` directory.

> [!div class="see-also"]
> <xref:configuration>
> <xref:dotnet-tool>
> <xref:troubleshooting-unattended-build>
> <xref:profiling>
> <xref:process-dump>


