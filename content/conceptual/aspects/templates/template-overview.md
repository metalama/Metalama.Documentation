---
uid: template-overview
level: 200
summary: "This document provides an overview of T#, a template language used by Metalama, which is fully compatible with C#. It details how T# integrates compile-time and run-time expressions and statements, and outlines the different scopes of code: run-time, compile-time, and scope-neutral. The document also compares T# to Razor and explains the compilation process."
keywords: "T#, Metalama, template language, compile-time expressions, run-time expressions, compile-time code, run-time code, scope-neutral code, compilation process"
created-date: 2023-02-17
modified-date: 2024-08-04
---

# T# templates: Overview

T# is the template language used by Metalama. The syntax of T# is 100% compatible with C#. The distinction between T# and C# lies in the fact that the T# compiler operates within the compiler or the IDE to generate C# code, while the C# compiler generates IL (binary) files.

## Scopes of code

T# templates integrate _compile-time_ and _run-time_ expressions and statements. Compile-time expressions and statements are evaluated at compile time in the compiler (or at design time in the IDE using the Diff Preview feature) and result in the generation of other run-time expressions.

Metalama analyzes T# and separates the compile-time portion from the run-time portion through the application of inference rules. Compile-time expressions and statements typically initiate with the `meta` pseudo-keyword. <xref:Metalama.Framework.Aspects.meta> is technically a static class, but it's helpful to perceive it as a magic keyword that initiates a compile-time expression or statement.

In Metalama, every type in your source code belongs to one of the following _scopes_:

### Run-time code

_Run-time code_ compiles to a binary assembly and typically executes on the end user's device. In a project that does not reference the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package, all code is considered run-time.

The entry point of run-time code is typically the _Program.Main_ method.

### Compile-time code

_Compile-time code_ is executed either at compile time by the compiler or at design time by the IDE.

Metalama recognizes compile-time-only code thanks to the <xref:Metalama.Framework.Aspects.CompileTimeAttribute> custom attribute. It searches for this attribute not only on the member but also on the declaring type, and at the base types and interfaces. Most classes and interfaces of the _Metalama.Framework_ assembly are compile-time-only.

You can create compile-time classes by annotating them with <xref:Metalama.Framework.Aspects.CompileTimeAttribute>.

> [!WARNING]
> All compile-time code _must_ be strictly compatible with .NET Standard 2.0, even if the containing project targets a more advanced platform. Any call to an API that is not strictly .NET Standard 2.0 will be considered run-time code.

### Scope-neutral code

_Scope-neutral code_ can execute either at run time or at compile time.

Scope-neutral code is annotated with the <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute> custom attribute.

Aspect classes are scope-neutral because aspects are a unique kind of class. Aspects are typically represented as custom attributes, which can be accessed at run time using _System.Reflection_, but they are also instantiated at compile time by Metalama. Therefore, it is crucial that the constructors and public properties of the aspects are both run-time and compile-time.

However, some methods of aspect classes are purely compile-time. They cannot be executed at run time because they access APIs that exist only at compile time. These methods must be annotated with <xref:Metalama.Framework.Aspects.CompileTimeAttribute>.

## Initial example

Before proceeding, let's illustrate this concept with an example. The following aspect writes some text to the console before and after the execution of a method.

In the code below, compile-time code is highlighted <span class="metalamaClassification_CompileTime">differently</span> so you can see which part of the code executes at compile time and which at run time. In the different tabs on the example, you can see the aspect code (with the template), the target code (to which the aspect is applied), and the transformed code, which is the target code transformed by the aspect.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleLogging.cs name="Simple Logging"]

> [!NOTE]
> To benefit from syntax highlighting in Visual Studio, install the [Visual Studio Tools for Metalama](https://www.postsharp.net/links/download-unified-vsx). Syntax highlighting is not supported in other IDEs.

The expression `meta.Target.Method` (with an implicit trailing `.ToString()`) is a compile-time expression. At compile time, it is replaced by the name and signature of the method to which the aspect is applied.

The call to `meta.Proceed()` signifies that the original method body should be injected at that point.

### Comparison with Razor

T# can be compared to [Razor](https://learn.microsoft.com/aspnet/core/mvc/views/razor). Razor allows you to create dynamic web pages by mixing two languages: C# for server-side code (the _meta_ code), and HTML for client-side code. With T#, you also have two kinds of code: _compile-time_ and _run-time_ code. The compile-time code generates the _run-time_ code. The difference with Razor is that in T# both the compile-time and run-time code are the same language: C#. Metalama interprets every expression or statement in a template as having _either_ run-time scope _or_ compile-time scope. Compile-time expressions generally initiate by calls to the <xref:Metalama.Framework.Aspects.meta> API.

## Compilation process

When Metalama compiles your project, one of the first steps is to separate the compile-time code from the run-time code. From your initial project, Metalama creates two compilations:

1. The _compile-time_ compilation contains only compile-time code. It is compiled against .NET Standard 2.0. It is then loaded within the compiler or IDE process and executed at compile or design time.
2. The _run-time_ compilation contains the run-time code. It also contains the compile-time _declarations_, but their implementation is replaced by `throw new NotSupportedException()`.

During compilation, Metalama compiles the [T# templates](xref:templates) into standard C# code that generates the run-time code using the Roslyn API. This generated code, as well as any non-template compile-time code, is then zipped and embedded in the run-time assembly as a managed resource.

> [!WARNING]
> *Intellectual property alert.* The _source_ of your compile-time code is embedded in clear text, without any obfuscation, in the run-time binary assemblies as a managed resource.



