---
uid: previewing-transformed-code
level: 200
summary: "This document explains how to preview the code that Metalama generates for a few source files from the command line with the metalama preview command, without building the whole project. It is designed for AI coding agents and for environments without Visual Studio."
keywords: "metalama preview, transformed code, generated code, command line, AI agent, preview without building, Metalama Diff"
created-date: 2026-10-28
modified-date: 2026-10-28
---

# Previewing transformed code from the command line

The `metalama preview` command prints the code that Metalama generates for one or a few source files of a project. The code is formatted, so it can be read like the source code. The command does not build the project.

The command is designed for AI coding agents and for other tools that cannot use the Metalama Diff of Visual Studio. It is also useful in other IDEs and in a terminal. After an agent edits an aspect or the code it targets, the agent can preview the files it changed and check the result in a few seconds. It does not need to build the whole project and read the transformed files from the `obj` directory.

> [!NOTE]
> The command requires a project that uses Metalama 2027.0.5 or later.

## Installing the command

The `preview` command belongs to the Metalama command line tool. To install the tool, see <xref:dotnet-tool>.

## Previewing files

Pass the paths of the source files to the command:

```powershell
metalama preview src/MyProject/Services/OrderService.cs
```

The command finds the project of the file and prints the transformed code to the standard output. Each file starts with a header line that gives its path:

```text
// ===== C:\src\MyProject\Services\OrderService.cs =====
```

The header ends with `(not transformed)` when no aspect changes the file.

To preview several files, pass several paths. All the files must belong to the same project:

```powershell
metalama preview src/MyProject/Services/OrderService.cs src/MyProject/Services/CustomerService.cs
```

Diagnostics are written to the standard error output. The command returns a non-zero exit code when the compiler or Metalama reports an error. When the project has C# errors that do not prevent Metalama from transforming the code, the command prints the preview and the errors.

## Options

| Option | Description |
|--------|-------------|
| `--project <path>` | The project file. By default, the command uses the nearest project file in the directory of the first file or in a parent directory. |
| `-f`, `--framework <tfm>` | The target framework. By default, the command uses the first target framework of the project. The transformed code can differ between target frameworks. |
| `-c`, `--configuration <name>` | The build configuration. By default, the command uses the default configuration of the project. |
| `-o`, `--output <directory>` | A directory where the command writes the transformed files instead of printing them. |
| `--no-restore` | Does not restore the project. Use this option to save time when the project is already restored. |

## How the command works

The command performs the following steps:

1. MSBuild evaluates the project and returns the command line of the compiler. MSBuild does not compile the project. By default, MSBuild restores the project first.
2. The command runs the Metalama compiler of the project, from the `Metalama.Compiler` package that the project references. The code is therefore transformed by the version of Metalama that the project uses, not by the version of the command.
3. Metalama runs all aspects, fabrics and validators on the whole project. Aspects that are added from other files, for instance by a fabric or by aspect inheritance, are therefore included in the preview.
4. Metalama links and formats only the requested files. Then the compiler stops. It does not run the source generators and the analyzers, and it does not emit the assembly.

The command does not write the assembly of the project. It writes the transformed files to a temporary directory, and deletes this directory when it completes.

Because the command links and formats only a few files and does not emit the assembly, it is faster than a build of the project with formatted output, especially in large projects.

## Previewing the whole project

To read the transformed code of all files of a project, build the project with the `LamaDebug` configuration, or set the `MetalamaEmitCompilerTransformedFiles` and `MetalamaFormatOutput` MSBuild properties. The transformed files are then written to the `obj/<Configuration>/<TargetFramework>/metalama` directory. For details, see <xref:debugging-aspect-oriented-code> and <xref:msbuild-properties>.

## Limitations

* The projects that the project references must be built before the preview. The command does not build them. When an assembly of a referenced project is missing, the command reports it and stops.
* The command shows only the requested source files. It does not show the files that aspects add to the project.
* The formatting uses the default C# formatting rules. The settings of `.editorconfig` files are not applied.
* MSBuild runs in the process of the command. It uses the newest .NET SDK that the command can load, which can differ from the SDK that `global.json` selects for a build.

> [!div class="see-also"]
> <xref:understanding-your-code-with-aspects>
> <xref:debugging-aspect-oriented-code>
> <xref:ai-agents>
> <xref:dotnet-tool>
