---
uid: diff-tool
level: 300
summary: "This article explains how to configure the external diff tool used when aspect tests fail, including supported tools, DiffEngineTray, and CI/CD considerations."
keywords: "diff tool, DiffEngine, DiffEngineTray, aspect testing, snapshot testing, test failure, visual diff"
created-date: 2025-12-04
modified-date: 2025-12-04
---

# Configuring the external diff tool

By default, the test framework opens a visual diff tool when an aspect test fails—when the expected snapshot differs from the actual output. This feature uses the open-source [DiffEngine](https://github.com/VerifyTests/DiffEngine) library, which automatically detects and launches installed diff tools.

## Metalama test runner settings

To configure the test runner settings:

1. Install the `metalama` tool as described in <xref:dotnet-tool>.
2. Run the following command to open the configuration file in your default editor:

    ```powershell
    metalama config edit testRunner
    ```

This opens the `testRunner.json` file with the following default content:

```json
{
  "LaunchDiffTool": true,
  "MaxDiffToolInstances": 1
}
```

Available settings:

| Setting | Default | Description |
|---------|---------|-------------|
| `LaunchDiffTool` | `true` | Set to `false` to disable automatic diff tool launching. Useful for CI/CD pipelines. |
| `MaxDiffToolInstances` | `1` | Maximum number of diff tool instances that can be opened simultaneously. |

## Supported diff tools

DiffEngine automatically detects installed diff tools. Supported tools include:

- **Free:** Visual Studio Code, WinMerge, P4Merge, KDiff3, Meld, Vim, Neovim
- **Commercial:** Beyond Compare, Araxis Merge, Kaleidoscope (macOS), DeltaWalker, Rider
- **Integrated:** Visual Studio, TortoiseGit variants

For the complete list, see the [DiffEngine documentation](https://github.com/VerifyTests/DiffEngine#supported-tools).

## Selecting the diff tool

DiffEngine checks for tools in a predefined order. To prioritize a specific tool, set the `DiffEngine_ToolOrder` environment variable:

```powershell
# Windows PowerShell - prioritize Visual Studio Code
$env:DiffEngine_ToolOrder = "VisualStudioCode"

# Or specify multiple tools in order of preference
$env:DiffEngine_ToolOrder = "BeyondCompare,VisualStudioCode,WinMerge"
```

To permanently set this, add the environment variable to your system or user environment variables.

## Using DiffEngineTray (Windows)

For a better workflow, install [DiffEngineTray](https://github.com/VerifyTests/DiffEngine/blob/HEAD/docs/tray.md), a Windows system tray application that tracks pending snapshot changes:

```powershell
dotnet tool install -g DiffEngineTray
diffenginetray
```

DiffEngineTray provides:

- **Accept/Discard buttons:** Quickly accept or discard pending changes from the system tray
- **Batch operations:** Accept or discard all pending changes at once
- **Keyboard shortcuts:** Configurable hotkeys for common operations
- **Auto-start:** Option to run at Windows startup

## Disabling diff tools for CI/CD

On build servers, you typically want to disable diff tool launching. DiffEngine automatically detects common CI environments (GitHub Actions, Azure DevOps, Jenkins, TeamCity, etc.) and disables itself.

To explicitly disable, use one of these methods:

1. Set the Metalama setting in `testRunner.json`:

    ```json
    { "LaunchDiffTool": false }
    ```

2. Set the DiffEngine environment variable:

    ```powershell
    $env:DiffEngine_Disabled = "true"
    ```

## ReSharper and Rider

When running tests in ReSharper or Rider with diff tools enabled, you may see an "orphaned processes" dialog when tests complete while diff tools are still open. To prevent this:

1. Open **Tools** → **Options** → **Unit Testing** (ReSharper) or **Settings** → **Unit Testing** (Rider)
2. Disable **Check for orphaned processes spawned by test runner**

> [!div class="see-also"]
> <xref:aspect-testing>
> <xref:dotnet-tool>
