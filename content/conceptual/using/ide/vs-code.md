---
uid: ide-vs-code
level: 100
summary: "Configure VS Code for use with Metalama, including enabling Roslyn analyzers and code fixes."
keywords: "Metalama VS Code, VS Code analyzers, Roslyn analyzers, code fixes VS Code, C# Dev Kit"
created-date: 2025-12-04
modified-date: 2025-12-04
---

# Configuring VS Code

VS Code supports Metalama's core functionality but requires configuration to enable code fixes and refactorings.

## Enabling Roslyn analyzers and code fixes

By default, VS Code doesn't run Roslyn analyzers, so Metalama's code fixes and refactorings won't appear. You'll need to configure the C# Dev Kit extension.

### Using C# Dev Kit

If you're using the [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension:

1. Open VS Code settings (**File** > **Preferences** > **Settings** or press **Ctrl+,**).
2. Search for `dotnet.backgroundAnalysis.analyzerDiagnosticsScope`.
3. Set it to `fullSolution` or `openFiles`.

> [!WARNING]
> Enabling Roslyn analyzers increases memory usage and may slow down the editor on large solutions. If you experience performance issues, consider limiting analysis to open files only.

## Limitations

The following Metalama features aren't available in VS Code:

- **CodeLens**: Aspect information overlays
- **Diff preview**: Visual code transformation preview
- **Live templates**: Source code modification aspects

These features require the Metalama Tools for Visual Studio extension.

## Workspace configuration

For team consistency, add VS Code settings to your repository. Create or edit `.vscode/settings.json`:

```json
{
    "dotnet.backgroundAnalysis.analyzerDiagnosticsScope": "fullSolution"
}
```

> [!div class="see-also"]
>
> <xref:code-fixes>
>
> <xref:ide-configuration>
