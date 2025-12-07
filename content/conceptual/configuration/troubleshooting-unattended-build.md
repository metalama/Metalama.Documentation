---
uid: troubleshooting-unattended-build
summary: "This article shows how to enable logging and process dumps for an unattended build on a build server without installing the metalama tool."
keywords: "unattended build, build server, logging, process dumps, diagnostics.json, environment variable, METALAMA_DIAGNOSTICS, METALAMA_CONSOLE_TRACE, dotnet build, msbuild"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Troubleshooting an unattended build

This article explains how to enable logging and process dumps in an unattended build on a build server without installing the `metalama` tool.

You can log to files or to the console.

## Approach 1. Logging to files

### Step 1. Create diagnostics.json on your local machine

Refer to the other articles in this section to learn how to create a `diagnostics.json` file for troubleshooting a specific scenario.

This example shows the `diagnostics.json` file after editing. Logging is enabled for the compiler process and all categories.

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

Metalama automatically reads the diagnostics configuration from the environment variable. The build produces diagnostics based on the configuration specified in the environment variable.

### Step 4. Download the logs

Find the logs under the `%TEMP%\Metalama\Logs` directory.

> [!NOTE]
> To store temporary files in a different directory than `%TEMP%`, set the `METALAMA_TEMP` environment variable.

## Approach 2. Logging to the console

To diagnose builds on build agents, the above procedure may be cumbersome because you need to upload the logs from the build agent to an artifact repository.

Logging directly to the console may be preferable. The inconvenience of this approach is that the text output can be huge and difficult to parse.

To enable console logging, set the `METALAMA_CONSOLE_TRACE` environment variable to `*` or to a comma-separated list of trace categories.

The `dotnet build` or `msbuild` process, as well as the Metalama compiler process, reuses background processes by default. These processes may fail to receive the `METALAMA_CONSOLE_TRACE` environment variable. To ensure that the Metalama compiler process receives the environment variable, disable build servers using the `--disable-build-servers` flag.

It's also important to enable detailed verbosity in `dotnet build` or `msbuild` because the default verbosity doesn't pass through the standard output of the compiler process.

### Example: PowerShell

This example shows how to enable console logging for all categories:

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

> [!div class="see-also"]
> <xref:configuration>
> <xref:creating-logs>
> <xref:process-dump>
> <xref:profiling>
