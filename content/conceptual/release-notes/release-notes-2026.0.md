---
uid: release-notes-2026.0
summary: ""
keywords: "Metalama 2026.0, release notes"
created-date: 2025-11-01
modified-date: 2025-11-07
---

# Metalama 2026.0

We are thrilled to announce Metalama 2026.0! This major version brings full support for C# 14—the most significant evolution of the C# language in many years.

**Highlights:**

- **C# 14 and .NET 10 SDK support**, including extension blocks, partial constructors and events, and compound assignment operators
- **First-class tuple types** with direct access to element names and types
- **Event handler invocation overriding** for implementing patterns like safe events
- **Faster Visual Studio experience** with significant performance improvements

Metalama 2026.0 ensures you are ready to take full advantage of the latest C# features while keeping your aspects clean, powerful, and maintainable.

## Requirements

### Development environment

Metalama 2026.0 supports the following development environments and SDKs:

- Visual Studio:
    - 2022 LTSC 17.12 (latest build), or
    - 2022 17.14 (latest build), or
    - 2026 18.0 (latest build).
- .NET SDK 8.0, 9.0, or 10.0.
- C# 12, 13, or 14.

> [!WARNING]
> .NET 6 SDK has been deprecated in this release.

### Target frameworks (runtimes)

Metalama 2026.0 supports the following target frameworks:

- **Metalama.Framework** and **Metalama.Extensions**: any framework implementing the .NET Standard 2.0 API (language polyfills might be required for some frameworks, see for instance [PolySharp](https://github.com/Sergio0694/PolySharp)).
- **Metalama.Patterns**: .NET Framework 4.7.2 (tested), .NET 8.0 (tested), or any framework implementing the .NET Standard 2.0 API (untested).

> [!WARNING]
> .NET 6 has been deprecated as a tested runtime.


## C# 14 Support

Metalama 2026.0 provides extensive support for C# 14 language features. While most features are fully implemented, some remain on the roadmap for future releases.

### Implemented in 2026.0

Here is what you can already do in Metalama:

- [#1108](https://github.com/metalama/Metalama/issues/1108): Use null-conditional assignments when generating syntax from an <xref:Metalama.Framework.Code.IFieldOrPropertyOrIndexer> (when assigning their `Value` property). Use the <xref:Metalama.Framework.Code.Invokers.IFieldOrPropertyInvoker.WithOptions*> and specify `NullConditional`.
- [#1094](https://github.com/metalama/Metalama/issues/1094): Override a property that uses the `field` keyword.
- [#1110](https://github.com/metalama/Metalama/issues/1110): Override or introduce to a partial constructor.
- [#1111](https://github.com/metalama/Metalama/issues/1111): Add an instance initializer to a partial constructor.
- [#1112](https://github.com/metalama/Metalama/issues/1112): Introduce partial events.
- [#1113](https://github.com/metalama/Metalama/issues/1113): Override partial events.
- [#1034](https://github.com/metalama/Metalama/issues/1034): Query extension blocks and extension members from the code model (see below).
- [#1035](https://github.com/metalama/Metalama/issues/1035): Override members of extension blocks.
- [#1115](https://github.com/metalama/Metalama/issues/1115): Query compound assignment operators in the code code model.
- [#1116](https://github.com/metalama/Metalama/issues/1116): Override compount assignment operators.
- [#1160](https://github.com/metalama/Metalama/issues/1160): Introduce new extension members into existing extension blocks
- [#1041](https://github.com/metalama/Metalama/issues/1041): Use simple lambda parameters with modifiers both in compile-time and run-time code.
- [#1105](https://github.com/metalama/Metalama/issues/1105): When an unsupported feature is used in a template, a understandable error message will be reported.

### Limitations

The following C# 14 features have not been implemented in Metalama 2026.0:

- [#1109](https://github.com/metalama/Metalama/issues/1109): Use null-conditional assignments in templates.
- [#1114](https://github.com/metalama/Metalama/issues/1114): Use the `field` keyword in templates.
- [#1036](https://github.com/metalama/Metalama/issues/1036): Generate run-time code for extension members using invoker interfaces.
- [#1127](https://github.com/metalama/Metalama/issues/1127): Add a contract to the receiver parameter of extension blocks.
- [#1131](https://github.com/metalama/Metalama/issues/1131): Introducing new compound assignment operators.
- [#1143](https://github.com/metalama/Metalama/issues/1143): Introducing parameters into partial constructors.
- [#1159](https://github.com/metalama/Metalama/issues/1159): Introducing new extension blocks.


## Extension blocks

Extension blocks represent the flagship feature of C# 14, enabling the extension of any type with new members. Metalama 2026.0 provides comprehensive support for extension blocks, including the ability to override extension members.

Extension blocks are modeled using the <xref:Metalama.Framework.Code.IExtensionBlock> interface, which derives from <xref:Metalama.Framework.Code.INamedType> with the following characteristics:

- <xref:Metalama.Framework.Code.ICompilationElement.DeclarationKind> is `Extension` and <xref:Metalama.Framework.Code.IType.TypeKind> is `Extension`.
- Adds <xref:Metalama.Framework.Code.IExtensionBlock.ReceiverParameter> and <xref:Metalama.Framework.Code.IExtensionBlock.ReceiverType> properties.
- For extension members (excluding classic extension methods), <xref:Metalama.Framework.Code.IMember.DeclaringType?text=IMember.DeclaringType> references the <xref:Metalama.Framework.Code.IExtensionBlock>.

> [!WARNING]
> Although <xref:Metalama.Framework.Code.IExtensionBlock> implements <xref:Metalama.Framework.Code.INamedType>, an extension block _cannot_ be used as an <xref:Metalama.Framework.Code.IType>. This behavior breaks the Liskov Substitution Principle, but it is much simpler than changing the type of <xref:Metalama.Framework.Code.IMember.DeclaringType?text=IMember.DeclaringType> property.

Extension blocks are accessible through <xref:Metalama.Framework.Code.INamedType.ExtensionBlocks?text=INamedType.ExtensionBlocks>, _not_ as nested types in <xref:Metalama.Framework.Code.INamespaceOrNamedType.Types?text=INamedType.Types>.

Extension methods and property accessors are available in <xref:Metalama.Framework.Code.INamedType.Methods?text=INamedType.Methods> as implicitly-implemented methods. These methods exist in IL and are addressable in C#, but cannot be overridden with Metalama.

This implementation aligns closely with Roslyn's model, providing a natural experience for C# developers.

## First-class support for tuple types

Metalama 2026.0 introduces first-class support for named tuples through the new <xref:Metalama.Framework.Code.ITupleType> interface. Previously, tuples were treated as plain <xref:Metalama.Framework.Code.INamedType> objects without access to element types or names, making scenarios such as argument packing for interceptors cumbersome and inefficient.

The new implementation provides direct access to element names and types through <xref:Metalama.Framework.Code.TypeFactory.CreateTupleType*?text=TypeFactory.CreateTupleType> for creation, and <xref:Metalama.Framework.Code.ITupleType.CreateCreateInstanceExpression*?text=ITupleType.CreateCreateInstanceExpression> for instantiation.

The implementation supports tuples with any number of elements:

- For tuples with two or more elements: native tuple syntax
- For degenerate cases (zero or one element): automatic fallback to `ValueTuple.Create(...)`

For details, see <xref:type-system>.

## Event handler invocation overriding

Metalama 2026.0 introduces the capability to override event handler invocations. This extends the existing functionality that allowed overriding only the add and remove operations of events.

This new advice kind allows you to implement aspects such as "safe events", where event handlers are isolated one from the other by an exception handler.

For comprehensive documentation, see <xref:overriding-events>.

## Visual Studio Tools for Metalama: performance improvements

We have refactored several components of Visual Studio Tools for Metalama to improve its performance, sometimes dramatically. It should now be more stable, consume less CPU, and make better use of your cores.


## Additional improvements

* **User-defined checked operators.** Metalama 2026.0 adds support for introducing user-defined `checked` operators.

* **Cross-project dependency injection.** Enhanced dependency injection capabilities now allow pulling constructor parameters across project boundaries. See <xref:dependency-injection>.

* **Compile-time assembly downloader.** The component that downloads compile-time assembly now properly respects the project's `nuget.config` file for package resolution.

## Documentation updates

* New article: <xref:type-system>.
* Improved the chapter: <xref:templates>.
* Improved the API documentation by adding elements from the conceptual documentation where relevant.

## Breaking changes

- <xref:Metalama.Framework.Code.IType.TypeKind?text=INamedType.TypeKind> now returns `Tuple` instead of `NamedType` for tuples.
- <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice?text=IAspectBuilder.Advice> is now obsolete. Use <xref:Metalama.Framework.Aspects.IAdviser> instead.
- The <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.With*> method has been split into <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithObject*> and <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*> with additional overloads. This change applies to all kinds of members.
- <xref:Metalama.Framework.Aspects.IAdviser> and <xref:Metalama.Framework.Aspects.AdviserExtensions> have been moved to the `Metalama.Framework.Aspects` namespace.
- `TypeKind.RecordClass` and `TypeKind.RecordStruct` have been removed and replaced by <xref:Metalama.Framework.Code.INamedType.IsRecord?text=INamedType.IsRecord>.


