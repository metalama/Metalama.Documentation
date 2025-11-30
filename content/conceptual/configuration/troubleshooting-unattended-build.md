---
uid: troubleshooting-unattended-build
summary: "The document provides instructions on how to enable logging and process dumps for an unattended build on a build server without installing the 'metalama' tool."
keywords: "unattended build, build server, logging, process dumps, diagnostics.json, environment variable, METALAMA_DIAGNOSTICS, METALAMA_CONSOLE_TRACE, dotnet build, msbuild"
created-date: 2023-01-26
modified-date: 2024-08-04
---

# Troubleshooting an unattended build

This article describes the process of enabling logging and processing dumps in an unattended build on a build server without installing the `metalama` tool.

Two approaches are available: logging to files, or to the console.

## Approach 1. Logging to files

### Step 1. Create diagnostics.json on your local machine

You can refer to the other articles in this chapter to learn how to create a `diagnostics.json` file for troubleshooting a specific scenario.

In the following example, we present the resultant `diagnostics.json` file after editing. Here, logging is enabled for the compiler process and all categories.

```json
{
  "logging": {
    "processes": {
      "Compiler": true
    },
    "categories": {
      "*": true
    }
  }
}
```

### Step 2. Copy diagnostics.json to the METALAMA_DIAGNOSTICS environment variable

In your build or pipeline configuration, create an environment variable named `METALAMA_DIAGNOSTICS` and set its value to the content of the `diagnostics.json` file.

> [!WARNING]
> Using diagnostics set by an environment variable always overrides local diagnostics settings used by the `metalama` tool.

### Step 3. Run the build on the build server

Metalama will automatically read the diagnostics configuration from the environment variable. The build will produce diagnostics based on the configuration set specified in the environment variable.

### Step 4. Download the logs

You can find the logs under the `%TEMP%\Metalama\Logs` directory.

> [!NOTE]
> To store temporary files in a different directory than `%TEMP%`, set the `METALAMA_TEMP` environment variable.


## Approach 2. Logging to the console 

To diagnose the build on build agents, the above procedure may be cumbersome because of the need to upload the logs from the build agent to some artifact repository.

It may be preferable to log directly to the console instead. The inconvenience of this approach is that the text output can be huge and difficult to parse.

To enable console logging, set the `METALAMA_CONSOLE_TRACE` environment variable to `*` or to a comma-separated list of trace categories.

Note that `dotnet build` or `msbuild` process, as well as the Metalama compiler process, reuse background processes by default. These processes may fail to receive the `METALAMA_CONSOLE_TRACE` environment variable. To ensure that the Metalama compiler process receives the environment variable, you must disable build servers using the `--disable-build-servers` flag.

It is also important to enable detailed verbosity in `dotnet build` or `msbuild` because the default verbosity doesn't pass through the standard output of the compiler process.

### Example: PowerShell
Combining all these notes, here is how to enable console logging for all categories:

```powershell
$env:METALAMA_CONSOLE_TRACE="*"
dotnet build -t:rebuild --disable-build-servers -v:detailed
```

### Example: GitHub action

```yaml
name: Build and Test
on:
    push:
        branches:
        - master
env:
    METALAMA_CONSOLE_TRACE: '*'
jobs:
    build-and-test:
        strategy:
            fail-fast: false
            matrix:
                os: [ubuntu-latest, windows-latest, macos-latest]
                dotnet-version: ['8.x']
        runs-on: ${{ matrix.os }}
        name: Build and Test on ${{ matrix.os }} with .NET Core ${{ matrix.dotnet-version }}

        steps:
            - uses: actions/checkout@v4
            - name: Setup .NET Core
              uses: actions/setup-dotnet@v4
              with:
                  dotnet-version: ${{ matrix.dotnet-version }}
            - run: dotnet restore
            - run: dotnet build --configuration Debug --no-restore -v:detailed --disable-build-servers
            - run: dotnet test --configuration Release --no-restore -v:detailed --disable-build-servers 
```


