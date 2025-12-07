---
uid: ide-visual-studio
level: 100
summary: "Configure Visual Studio for optimal use with Metalama, including the Metalama Tools extension."
keywords: "Metalama Visual Studio, Visual Studio extension, Metalama Tools, CodeLens, diff preview, live templates"
created-date: 2025-12-04
modified-date: 2025-12-04
---

# Configuring Visual Studio

Visual Studio provides the most complete Metalama experience. All features work out of the box once you install the Metalama Tools extension.

## Installing Metalama Tools for Visual Studio

The Metalama Tools extension adds several IDE features:

- **CodeLens**: Shows aspect information directly in the editor
- **Diff preview**: Visualize how aspects transform your code
- **Live templates**: Apply aspects that modify source code directly
- **Code fixes and refactorings**: Apply aspects through the lightbulb menu

For installation instructions, see <xref:install-vsx>.

## Recommended settings

### Disabling lightweight solution load for large solutions

If you have a large solution with many projects using Metalama, consider disabling lightweight solution load to ensure all projects are fully loaded and analyzed:

1. Open **Tools** > **Options** > **Projects and Solutions** > **General**.
2. Clear the **Load projects in lightweight mode** checkbox.

### Configuring source link for debugging

To step into Metalama source code when debugging:

1. Open **Tools** > **Options** > **Debugging** > **General**.
2. Select the **Enable Source Link support** checkbox.
3. Clear the **Enable Just My Code** checkbox.

## Troubleshooting

### Extension not loading

If the Metalama Tools extension isn't working:

1. Verify the extension is installed: **Extensions** > **Manage Extensions** > **Installed**.
2. Check the extension is enabled for your Visual Studio instance.
3. Try restarting Visual Studio.
4. Check the Activity Log for errors: run `devenv /log` and review `%APPDATA%\Microsoft\VisualStudio\<version>\ActivityLog.xml`.

### CodeLens not showing

If CodeLens information isn't appearing:

1. Verify CodeLens is enabled: **Tools** > **Options** > **Text Editor** > **All Languages** > **CodeLens**.
2. Check that **Show Metalama CodeLens** is enabled.
3. Ensure the file is part of a project with Metalama enabled.

> [!div class="see-also"]
>
> <xref:install-vsx>
>
> <xref:understanding-your-code-with-aspects>
>
> <xref:ide-configuration>
