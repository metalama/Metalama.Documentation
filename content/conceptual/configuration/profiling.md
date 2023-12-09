---
uid: profiling
level: 200
---

# Capturing performance data

If you are experiencing performance issues with Metalama, our support team might request to profile the Metalama or IDE processes.

> [!WARNING]
> **Profiling snapshots may contain potentially confidential information**
>
> Profiling snapshots can include call stacks from your compile-time code. While we treat process dumps as confidential material, your company might not permit you to send us a profiling snapshot without management approval.

> [!NOTE]
> Metalama uses [JetBrains dotTrace](https://www.jetbrains.com/profiler/) to create performance snapshots. dotTrace will be automatically downloaded upon first use. You do not need a license to collect performance, but you may need to acquire a license if you want to analyze this data.

## Step 1. Install the PostSharp Command-Line Tool

Install the `metalama` command-line tool following the instructions in <xref:dotnet-tool>.

## Step 2. Edit diagnostics.json

Run the command:

```powershell
metalama config edit diagnostics
```

This command should open a `diagnostics.json` file in your default editor.

The `profiling/processes` section lists processes that need to be profiled. The values are `false` by default, and you can set them to `true` for the processes you want to profile:

* `Compiler`: the compile-time process.
* `Rider`: the design-time Roslyn process running under Rider.
* `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
* `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).

In the example below, Metalama is set up to profile the compiler process.

```json
{
 // ...
"profiling": {
    "processes": {
     "DotNetTool": false,
      "BackstageWorker": false,
      "OmniSharp": false,
      "Compiler": true,
      "TestHost": false,
      "CodeLensService": false,
      "Other": false,
      "ResharperTestRunner": false,
      "DevEnv": false,
      "Rider": false,
      "RoslynCodeAnalysisService": false
    }
//...
}
```

## Step 3. Execute Metalama

Restart the profiled processes:

* If you enabled profiled for the `Compiler` process, restart the Roslyn compiler processes using `metalama kill`.
* If you enabled profiled for any design-time processes, restart your IDE.

Perform the actions that cause the issue.

> [!WARNING]
> Remember to disable the diagnostic setting once you have finished.

## Step 4. Stop the profiled processes

Close your IDE. If you are profiling the compiler processes, run `metalama kill`.

Wait a file with extension `*.dtp` is created under the `%TEMP%\Metalama\Profiling` directory. 

## Step 5. Upload the snapshots to an online drive

You will find the profiling snapshots in the `%TEMP%\Metalama\Profiling` directory. Zip the whole directory and upload this file to an online storage service like OneDrive.

## Step 6. Send us the URL through a private channel

> [!WARNING]
> **NEVER** share the snapshot URL publicly on a service like GitHub Issues.

Instead, kindly send us the link via [email](mailto:hello@postsharp.net) or private message on [Slack](https://www.postsharp.net/slack).
