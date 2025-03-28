---
uid: debugging-aspects
level: 300
summary: "The document provides step-by-step instructions on how to debug compile-time and design-time logic in aspect-oriented programming, emphasizing the importance of inserting breakpoints directly into the source code."
keywords: "debug compile-time logic, design-time logic, aspect-oriented programming, breakpoints, Debugger.Break(), meta.DebugBreak(), attach debugger, Metalama Command Line Tool, RoslynCodeAnalysisService, MetalamaDebugCompiler"
created-date: 2023-02-17
modified-date: 2024-08-29
---

# Debugging aspects

## Adding breakpoints to templates and compile-time code

Debugging the compile-time logic of an aspect can be challenging due to the compiler not executing your _source_ code, but a heavily transformed version of your code where T# templates have been compiled into plain C# code. This transformed code is stored under an unpredictable path.

Therefore, regular debugger breakpoints will not work. You must add break statements directly in your source code and remember to remove them after the debugging session is over.

- In a _non-template_ compile-time method such as `BuildAspect`, invoke <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
- In a _template_ compile-time method, invoke <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

## Debugging aspect tests

The most convenient way to debug an aspect is to create an _aspect test_ as described in @aspect-testing. This allows you to perfectly isolate the scenario that you want to debug.

To debug an aspect test:

1. Insert breakpoints directly into your source code as described above.
2. Use the _Debug_ command of the unit test runner.

## Debugging the compiler process

To debug compile-time logic, follow the steps below:

1. Insert breakpoints directly into your source code as described above.

2. Execute the compiler with the following options:

    * `-p:MetalamaDebugCompiler=True` to cause the compiler to display the JIT debugger dialog, allowing you to attach a debugger to the compiler process.
    * `-p:MetalamaConcurrentBuildEnabled=False` to force Metalama to run in a single thread, saving you from the chaos of multi-threaded debugging.
    * Optionally, `--disable-build-servers` to disable the use of reusable server MSBuild and `Metalama.Compiler` processes.
    * Optionally, `--no-dependencies` to avoid rebuilding referenced projects.
    
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

