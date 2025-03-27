---
uid: release-notes-2023.3
summary: "Metalama 2023.3 release introduces new features including ready-made aspect implementations for caching and contracts, auxiliary templates, and a revamped Metalama.Framework.Sdk. It also includes platform updates, enhancements, minor breaking changes, and bug fixes."
keywords: "Metalama 2023.3, release notes"
created-date: 2023-12-11
modified-date: 2024-08-04
---

# Metalama 2023.3

Metalama 2023.3 introduces several new features: ready-made aspect implementations for caching and contracts, auxiliary templates, greatly revamped `Metalama.Framework.Sdk`, and several enhancements and bug fixes.

## Platform updates

* Roslyn 4.7.
* Visual Studio Code:
    - C# Dev Kit is now supported.
    - Omnisharp is deprecated and no longer tested.
* Visual Studio for Mac is deprecated and no longer tested, as Microsoft announced its sunsetting.

## New Features

### Auxiliary templates

It is now possible to call a template from a template. This allows to remove redundancy in templates and use abstraction and encapsulation thanks to `virtual` templates and delegate-like template invocations.

For details, see <xref:auxiliary-templates>.

### Metalama.Patterns.Contracts

Metalama Contracts y is an [open-source](https://github.com/postsharp/Metalama.Patterns/tree/release/2023.3/src/Metalama.Patterns.Contracts), aspect-oriented implementation of [System.ComponentModel.DataAnnotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations). Unlike Microsoft's annotations, Metalama Contracts works with any C# code, not just ASP.NET MVC or Entity Framework, as it utilizes aspects to inject validation logic during compilation.

In 2023.3, we are releasing `Metalama.Patterns.Contracts` under the _preview_ quality label. Conceptual documentation is not available at the moment. For conceptual documentation, see <xref:Metalama.Patterns.Contracts>.

### Metalama.Patterns.Caching

We've also ported our PostSharp-based caching framework to Metalama and completely [open-sourced](https://github.com/postsharp/Metalama.Patterns/tree/release/2023.3/src/Metalama.Patterns.Caching) it. We updated the codebase to take full advantage of modern .NET and C#, including the use of `IReadOnlySpan<char>` to further reduce garbage collection load.

As for contracts, we are currently releasing `Metalama.Patterns.Caching` under the _preview_ quality label. Conceptual documentation is not available at the moment. For conceptual documentation, see <xref:Metalama.Patterns.Caching>.

### Metalama.Framework.Sdk

You can now seamlessly use the Roslyn API for low-level code model analysis using the `Metalama.Framework.Sdk` package. Although this package was available before, it wasn't directly accessible from an aspect project. That limitation has been removed.

For details, see <xref:sdk>.

## Enhancements

### Debugging and troubleshooting

* In the error list, with most errors and warnings reported by aspects, you can now see which aspect class and target declaration reported the diagnostic.
* It is now possible to enable performance profiling of Metalama processes. See <xref:profiling> for details.
* Visual Studio Tools for Metalama has improved reporting of errors in compile-time code.
* The debugging experience of templates and compile-time code has been dramatically improved. However, it is still required to use `Debugger.Break()` or `meta.DebugBreak()` to add an initial breakpoint.

### Object model

* The <xref:Metalama.Framework.Code.IDeclaration> interface has a new <xref:Metalama.Framework.Code.IDeclaration.Sources> property exposes references to the source code. You can now get any declaration's file path, line, and column.
* The <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*> method has a new overload that accepts an  `IEnumerable<IExpression>` to generate dynamic method calls.
* Invoking a method from the target type with <xref:Metalama.Framework.Code.Invokers.InvokerOptions.Base?text=InvokerOptions.Base> and an instance (other than `base`) is now possible if the base layer is in the current type.
* Any aspect can now reflect on any other <xref:Metalama.Framework.Aspects.IAspectInstance> added before the current aspect thanks to the new API <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetAspectInstances?text=declaration.Enhancements().GetAspectInstances()> returning an `IEnumerable<IAspectInstance>`. The previous method, <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetAspects*>, did not return the <xref:Metalama.Framework.Aspects.IAspectInstance> and gave no access to the <xref:Metalama.Framework.Aspects.IAspectState>.
* <xref:Metalama.Extensions.DependencyInjection>: You can now introduce a dependency programmatically thanks to the <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.TryIntroduceDependency*> method.
* New <xref:Metalama.Framework.Aspects.RunTimeAttribute?text=[RunTime]> attribute that restricts to run-time the scope of a type that derives from a <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute?text=[RunTimeOrCompileTime]>  base class or interface.
* <xref:Metalama.Framework.Advising.IAdviceFactory>: introduction methods like <xref:Metalama.Framework.Advising.AdviceKind.IntroduceMethod> now return the existing member even for <xref:Metalama.Framework.Advising.AdviceOutcome.Ignore?text=AdviceOutcome.Ignore>.
* <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1.AsyncEnumerator> now has a <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1.AsyncEnumerator.Parent> property which gets the <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1> over which the enumerator enumerates. This allows methods such as <xref:Metalama.Framework.RunTime.RunTimeAspectHelper.BufferToListAsync*> (see below) to avoid creating a new <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1> when an enumerator is already based on an <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1>.
* The <xref:Metalama.Framework.RunTime.RunTimeAspectHelper.BufferToListAsync*>  method has a new overload accepting an `IAsyncEnumerator<T>` which buffers an async enumerator into an <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1>, and returns the list. This supports scenarios such as caching.

### Performance

* The design-time performance of reference validators has been improved.
* Performance improvement of <xref:Metalama.Framework.Workspaces>, especially useful while using the Metalama driver for LinqPad.

### Other

* Overriding asynchronous methods with a template has been improved.


## Breaking changes

There will be a few minor breaking changes in 2023.3. We think that there are still relatively few users and certainly little legacy code to maintain compatibility with, so we are prioritizing usability improvements over backward compatibility.


* T#: `foreach` and `while` expressions now give preference to run-time scope. 

    Previously, `while ( true )` would be interpreted as a compile-time loop. Now, it will be considered as a run-time loop.
    
    Previously, the way to get a compile-time `for` loop was to do a `foreach ( var in in Enumerable.Range(...) )`. Now, `Enumerable.Range(...)` will be interpreted as run-time by default because it is inside a `foreach`, so you need to specifically mark it as compile-time using  `foreach ( var in in meta.CompileTime( Enumerable.Range(...) ) )`.
    
    
* In <xref:Metalama.Framework.Eligibility.EligibilityExtensions>, the method overload `void MustSatisfy<T>( this IEligibilityBuilder<T> eligibilityBuilder, Action<IEligibilityBuilder<T>> requirement )` has been renamed to <xref:Metalama.Framework.Eligibility.EligibilityExtensions.AddRules*> in order to avoid ambiguities with the first and principal overload of <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*>.

* The `IDiagnosticSink` interface is now internal. The <xref:Metalama.Framework.Diagnostics.ScopedDiagnosticSink> type is now used in all public APIs.

* The type `SyntaxReference` has been renamed <xref:Metalama.Framework.Code.SourceReference>.


## Bug fixes & changelogs

For a detailed list of bugs fixed in this release, see the detailed changelogs:

- [2023.3-7-rc](https://github.com/orgs/postsharp/discussions/226)
- [2023.3.6-rc](https://github.com/orgs/postsharp/discussions/223)
- [2023.3.5-rc](https://github.com/orgs/postsharp/discussions/218)
- [2023.3.4-preview](https://github.com/orgs/postsharp/discussions/213)
- [2023.3.3-preview](https://github.com/orgs/postsharp/discussions/211)
- [2023.3.2-preview](https://github.com/orgs/postsharp/discussions/200)
- [2023.3.1-preview](https://github.com/orgs/postsharp/discussions/196)




