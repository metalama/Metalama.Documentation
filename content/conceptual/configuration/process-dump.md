---
uid: process-dump
level: 200
summary: "The document provides a step-by-step guide on how to create and share a process dump for troubleshooting issues with Metalama, including warnings about potential confidentiality of information."
keywords: "Metalama issues, troubleshooting, process dump, confidentiality, Metalama command-line tool, dotnet dump tool, diagnostics.json, process crash, process dump collection, upload process dump."
created-date: 2023-01-11
modified-date: 2024-08-04
---

# Creating a process dump

If you're encountering issues with Metalama, our support team might request a process dump of the compiler or IDE process.

> [!WARNING]
> **Process dumps may contain potentially confidential information**
>
> Process dumps could include a copy of your source code. While we treat process dumps as confidential material, your company might not permit you to send us a process dump without management approval.

## Step 1. Install the PostSharp Command-Line Tool

Install the `metalama` command-line tool following the instructions in <xref:dotnet-tool>.

## Step 2. Install the `dotnet dump` tool

Run the command below:

```powershell
dotnet tool install --global dotnet-dump
```

## Step 3. Edit diagnostics.json

Run the command:

```powershell
metalama config edit diagnostics
```

This command should open a `diagnostics.json` file in your default editor.

The `miniDump/processes` section lists processes for which process dumps need to be collected. The values are `false` by default. Set the values to `true` if you wish to collect the process dumps of the following processes if they crash:

* `Compiler`: the compile-time process.
* `Rider`: the design-time Roslyn process running under Rider.
* `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
* `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).

In the example below, Metalama is set up to capture a process dump for the compiler process.

```json
{
 // ...
"crashDumps": {
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

## Step 4. Execute Metalama

Restart the logged processes:

* If you enabled dumps for the `Compiler` process, restart the Roslyn compiler processes using `metalama kill`.
* If you enabled dumps for any design-time processes, restart your IDE.

Perform the actions that cause the issue.

> [!WARNING]
> Remember to disable the diagnostic setting once you've finished.

## Step 5. Upload the process dump to an online drive

You will find process dumps in the `%TEMP%\Metalama\CrashReports` directory with the extension `*.dmp.gz`.
Upload this file to an online storage service like OneDrive.

## Step 6. Send us the URL through a private channel

> [!WARNING]
> **NEVER** share the process dump URL publicly on a service like GitHub Issues.

Instead, kindly send us the link via [email](mailto:hello@postsharp.net) or private message on [Slack](https://www.postsharp.net/slack).



