---
uid: ide-configuration
level: 100
summary: "Learn how to configure Visual Studio, VS Code, and Rider for optimal use with Metalama."
keywords: "Metalama IDE, Visual Studio configuration, VS Code Metalama, Rider Metalama, IDE setup, Roslyn analyzers"
created-date: 2025-12-04
modified-date: 2025-12-04
---

# Configuring your IDE

Metalama works with any text editor or IDE since the core transformation happens during compilation. For the best design-time experience—including real-time warnings, code fixes, and syntax highlighting—you'll need to configure your IDE. This section covers Visual Studio, VS Code (with C# Dev Kit), and Rider. Other editors aren't tested.

## In this section

| Article | Description |
|---------|-------------|
| <xref:ide-visual-studio> | Configuration tips for Visual Studio users, including the Metalama Tools extension. |
| <xref:ide-vs-code> | How to enable Roslyn analyzers and code fixes in VS Code. |
| <xref:ide-rider> | Configuration for aspect testing and other Rider-specific settings. |
| <xref:ide-claude-code> | Installing the Metalama skill for AI-assisted aspect development. |

## Feature availability by IDE

| Feature | Visual Studio | VS Code | Rider |
|---------|---------------|---------|-------|
| Code transformation | Yes | Yes | Yes |
| Live templates | Yes | No | No |
| Code fixes and refactorings | Yes | Requires configuration | Yes |
| CodeLens | Yes | No | No |
| Diff preview | Yes | No | No |
| Aspect testing | Yes | Yes | Requires configuration |

> [!NOTE]
> Some features like live templates, CodeLens, and diff preview require the Metalama Tools for Visual Studio extension. See <xref:install-vsx> for installation instructions.
