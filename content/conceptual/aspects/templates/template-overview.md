---
uid: template-overview
level: 200
summary: "This document provides an overview of T#, a template language used by Metalama, which is fully compatible with C#. It details how T# integrates compile-time and run-time expressions and statements, and outlines the different scopes of code: run-time, compile-time, and Run-time-or-compile-time. The document also compares T# to Razor and explains the compilation process."
keywords: "T#, Metalama, template language, compile-time expressions, run-time expressions, compile-time code, run-time code, Run-time-or-compile-time code, compilation process"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# T# templates: overview

T# is the template language used by Metalama to generate code. Templates are methods that combine compile-time logic with run-time code generation: they execute at compile-time to produce the C# code that will run in your application.

The syntax of T# is fully compatible with C#, meaning any valid T# code is valid C# code. However, T# has different semantics: the T# compiler executes within the compiler or IDE to generate C# source code, while the C# compiler transforms source code into IL (binary) files.

## Understanding code scopes

T# templates mix _compile-time_ and _run-time_ expressions and statements. Compile-time expressions and statements are evaluated at compile time (in the compiler or at design time in the IDE using the Diff Preview feature) and generate run-time expressions for the transformed code.

Metalama analyzes T# code and separates compile-time portions from run-time portions using inference rules. Compile-time expressions and statements typically begin with the `meta` pseudo-keyword. <xref:Metalama.Framework.Aspects.meta> is a static class, but it serves as a marker that begins a compile-time expression or statement.

In Metalama, every type in your source code belongs to one of the following _scopes_:

### Run-time code

_Run-time code_ compiles to a binary assembly and typically executes on the end user's device. In a project that doesn't reference the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package, all code is considered run-time.

The entry point of run-time code is typically the `Program.Main` method.

### Compile-time code

_Compile-time code_ is executed either at compile time by the compiler or at design time by the IDE.

Metalama recognizes compile-time-only code through the <xref:Metalama.Framework.Aspects.CompileTimeAttribute> custom attribute. It searches for this attribute not only on the member but also on the declaring type and on base types and interfaces. Most classes and interfaces of the `Metalama.Framework` assembly are compile-time-only.

You can create compile-time classes by annotating them with <xref:Metalama.Framework.Aspects.CompileTimeAttribute>.

> [!WARNING]
> All compile-time code must be strictly compatible with .NET Standard 2.0, even if the containing project targets a more advanced platform. Any call to an API that is not strictly .NET Standard 2.0 will be considered run-time code.

### Run-time-or-compile-time code

_Run-time-or-compile-time code_ can execute either at run time or at compile time.

Run-time-or-compile-time code is annotated with the <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute> custom attribute.

Aspect classes are run-time-or-compile-time because they must be accessible in both contexts. Aspects are represented as custom attributes that can be accessed at run time using `System.Reflection`, but they're also instantiated at compile time by Metalama. Therefore, the constructors and public properties of aspects must be usable in both run-time and compile-time contexts.

However, some methods of aspect classes are purely compile-time. They can't execute at run time because they access APIs that exist only at compile time. These methods must be annotated with <xref:Metalama.Framework.Aspects.CompileTimeAttribute>.

## Example

The following aspect adds logging before and after the execution of a method.

In the code below, compile-time code is highlighted <span class="metalamaClassification_CompileTime">differently</span> so you can see which part of the code executes at compile time and which at run time. In the different tabs on the example, you can see the aspect code (with the template), the target code (to which the aspect is applied), and the transformed code, which is the target code transformed by the aspect.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleLogging.cs name="Simple Logging"]

> [!NOTE]
> To benefit from syntax highlighting in Visual Studio, install the Visual Studio Tools for Metalama. See <xref:ide-configuration> for IDE-specific information.

The expression `meta.Target.Method` (with an implicit trailing `.ToString()`) is a compile-time expression. At compile time, it's replaced by the name and signature of the method to which the aspect is applied.

The call to `meta.Proceed()` indicates that the original method body should be injected at that point.

### Comparison with Razor

If you're familiar with ASP.NET, T# can be compared to [Razor](https://learn.microsoft.com/aspnet/core/mvc/views/razor). Razor lets you create dynamic web pages by mixing two languages: C# for server-side code and HTML for client-side code. Similarly, T# mixes two kinds of code: _compile-time_ code that generates _run-time_ code.

The key difference is that in T# both the compile-time and run-time code use the same language: C#. Metalama analyzes templates and determines whether each expression or statement has run-time scope, compile-time scope, or both. Compile-time expressions typically begin with calls to the <xref:Metalama.Framework.Aspects.meta> API.

## Compilation process

When Metalama compiles your project, one of the first steps is to separate the compile-time code from the run-time code. From your initial project, Metalama creates two compilations:

1. The _compile-time_ compilation contains only compile-time code. It's compiled against .NET Standard 2.0. It's then loaded within the compiler or IDE process and executed at compile or design time.
2. The _run-time_ compilation contains the run-time code. It also contains the compile-time _declarations_, but their implementation is replaced by `throw new NotSupportedException()`.

During compilation, Metalama compiles the T# templates into standard C# code that generates the run-time code using the Roslyn API. This generated code, as well as any non-template compile-time code, is then zipped and embedded in the run-time assembly as a managed resource.

> [!WARNING]
> **Intellectual property alert.** The _source_ of your compile-time code is embedded in clear text, without any obfuscation, in the run-time binary assemblies as a managed resource.

> [!div class="see-also"]
> <xref:template-compile-time>
> <xref:template-parameters>
> <xref:templates>
> <xref:aspects>
> <xref:Metalama.Framework.Aspects.meta>
> <xref:Metalama.Framework.Aspects.CompileTimeAttribute>
> <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute>
