---
uid: debugging-aspects
level: 300
summary: "The document provides step-by-step instructions on how to debug compile-time and design-time logic in aspect-oriented programming, emphasizing the importance of inserting breakpoints directly into the source code."
keywords: "debug compile-time logic, design-time logic, aspect-oriented programming, breakpoints, Debugger.Break(), meta.DebugBreak(), attach debugger, Metalama Command Line Tool, RoslynCodeAnalysisService, MetalamaDebugCompiler"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Debugging aspects

## Adding breakpoints to templates and compile-time code

Debugging the compile-time logic of an aspect or fabric can be challenging because the compiler doesn't execute your _source_ code, but a heavily transformed version where T# templates have been compiled into plain C#. This transformed code is stored under the `obj/<Configuration>/<TargetFramework>/metalama` directory.

> [!WARNING]
> You **cannot** set compile-time breakpoints in your source code files if the project references the `Metalama.Framework` package and the `MetalamaEnabled` MSBuild property is not set to `False`. Visual Studio breakpoints placed in source files will not be hit because the debugger sees only the transformed code, not the original source.
>
> You must add break statements directly in your source code and remember to remove them after the debugging session. Once the debugger stops at a break statement and you step through the code, you'll be viewing the _transformed_ code. At that point, you can set breakpoints in that transformed code file for the remainder of your debugging session.

To add break statements:

- In a _non-template_ compile-time method such as `BuildAspect`, invoke <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
- In a _template_ compile-time method, invoke <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

## Debugging aspect tests

The most convenient way to debug an aspect is to create an _aspect test_ as described in <xref:aspect-testing>. This allows you to isolate the scenario you want to debug.

To debug an aspect test:

1. Insert breakpoints directly into your source code as described above.
2. Use the _Debug_ command of the unit test runner.

## Debugging the compiler process

To debug compile-time logic:

1. Insert breakpoints directly into your source code as described above.

2. Execute the compiler with these options:

    - `-p:MetalamaDebugCompiler=True` to cause the compiler to display the JIT debugger dialog, allowing you to attach a debugger to the compiler process.
    - `-p:MetalamaConcurrentBuildEnabled=False` to force Metalama to run in a single thread, saving you from the chaos of multi-threaded debugging.
    - Optionally, `--disable-build-servers` to disable the use of reusable server MSBuild and `Metalama.Compiler` processes.
    - Optionally, `--no-dependencies` to avoid rebuilding referenced projects.

Example:

```powershell
dotnet build MyProject.csproj -p:MetalamaDebugCompiler=True -p:MetalamaConcurrentBuildEnabled=False
```

## Debugging the IDE process

To attach a debugger to the design-time compiler process, follow these steps:

1. Install the Metalama Command Line Tool as instructed in <xref:dotnet-tool>.
2. Execute the command below:

   ```powershell
   metalama config edit diagnostics
   ```

3. In the `diagnostics.json` file, modify the `debugging/processes` section and enable debugging for the appropriate process. If you're using Visual Studio, this process is named `RoslynCodeAnalysisService`.

    ```json
     {

        // ...

        "debugger": {
            "processes": {
                 "Rider": false,
                  "RoslynCodeAnalysisService": true
            }
        }

        // ...

    }
    ```

4. Insert breakpoints directly into your source code as described above.
5. Restart your IDE.

> [!div class="see-also"]
> <xref:debugging-aspect-oriented-code>
> <xref:testing>
> <xref:aspect-testing>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.meta.DebugBreak*>
