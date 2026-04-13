---
uid: introducing-constructor-parameters
level: 400
summary: "This article explains how to introduce parameters to constructors and methods, including optional parameters, required parameters with source-compatibility constructors, and inserting parameters at specific positions."
keywords: "IntroduceParameter, constructor parameter, dependency injection, IConstructor, AdviserExtensions, default value, pullStrategy, required parameter, source-compatibility constructor, InsertParameter, IMethodBaseBuilder, ForwardSourceConstructors, ConstructorOverloadingStrategy"
created-date: 2023-02-20
modified-date: 2026-04-13
---

# Introducing constructor parameters

Most of the time, an aspect requires introducing a parameter to a constructor when it needs to retrieve a dependency from a dependency injection framework. In such situations, use the <xref:Metalama.Extensions.DependencyInjection> framework, as detailed in <xref:dependency-injection>.

Typically, implementations of dependency injection frameworks introduce parameters using the method outlined here.

## Introducing an optional parameter

To append an optional parameter (one that has a default value) to a constructor, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that accepts a `defaultValue` argument. This method requires several arguments: the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value.

Because the parameter has a default value, existing callers can omit the new argument, so source compatibility is preserved automatically — no forwarding constructor is needed.

The `pullStrategy` parameter allows you to specify the value passed to this parameter in other constructors that call the specified constructor, using the `: this(...)` or `: base(...)` syntax. This parameter accepts an <xref:Metalama.Framework.Advising.IPullStrategy> implementation. To create a pull strategy, use one of the factory methods of the <xref:Metalama.Framework.Advising.PullStrategy> class, such as <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> or <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*>.

### Example: optional parameter

The example below demonstrates an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and invokes the `IInstanceRegistry.Register(this)` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Introducing optional parameters"]

## Introducing a required parameter

To append a required parameter (one that has no default value) to a constructor, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that does _not_ accept a `defaultValue` argument.

Because existing callers cannot omit the new argument, the framework preserves both source and binary compatibility by generating a _source-compatibility constructor_: a compile-time stub with the pre-mutation signature that chains via `: this(...)` to the mutated constructor. This constructor is marked with <xref:Metalama.Framework.RunTime.SourceCompatibilityConstructorAttribute>.

### Overloading strategy

The `overloadingStrategy` parameter controls whether and how source-compatibility constructors are generated. It accepts an <xref:Metalama.Framework.Advising.IConstructorOverloadingStrategy> implementation.

The <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy> class provides two built-in strategies:

| Strategy | Description |
|----------|-------------|
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardSourceConstructors> | Generates a forwarding constructor for every source constructor that the framework mutates. This is the default when `overloadingStrategy` is `null`. |
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardDefaultConstructor> | Generates a forwarding constructor only when the mutated constructor is the parameterless constructor. This is useful for types that must remain constructible via `Activator.CreateInstance<T>()` or a `new()` generic constraint. |

Both strategies return a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy> that exposes a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy.WithObsoleteAttribute*> method. Use this method to decorate the generated forwarding constructor with `[Obsolete]`, signaling to downstream callers that they should migrate to the new constructor signature.

### Pull strategy for source-compatibility constructors

The source-compatibility constructor needs to supply a value for the introduced required parameter when chaining to the mutated constructor. This value comes from the <xref:Metalama.Framework.Advising.IPullStrategy> you provide.

Use <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> to supply a static expression (such as `DateTime.Now` or a factory method call) for the parameter value in the forwarding constructor.

For more advanced scenarios, implement <xref:Metalama.Framework.Advising.IPullStrategy> directly. Your implementation can detect whether it is being called for a source-compatibility constructor by using the <xref:Metalama.Framework.Code.ConstructorExtensions.IsSourceCompatibilityConstructor*> extension method.

### Example: required parameter with source-compatibility constructors

The following example demonstrates an aspect that introduces a required `DateTime creationTime` parameter to all constructors. The framework generates source-compatibility constructors that supply `DateTime.Now` as the default value.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceRequiredParameter.cs name="Introducing required parameters"]

## Introducing a required parameter on records

> [!WARNING]
> **Breaking change in 2026.1:** When `IntroduceParameter` targets a record's primary constructor, the introduced parameter is no longer materialized as part of the record's value shape by default. This means the parameter will not generate an auto-property, will not appear in `Deconstruct`, and will not participate in `Equals`, `GetHashCode`, or `ToString`.

This change prevents accidental pollution of a record's identity with infrastructure parameters (such as DI dependencies or contextual objects).

To restore the previous behavior, explicitly opt in by using `PullStrategy.IntroduceParameterAndPull(materializeOnRecord: true)`.

## Inserting parameters at a specific position

When introducing a method or constructor programmatically, you can insert parameters at a specific position in the parameter list using the <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBaseBuilder.InsertParameter*> method. This is useful when you need to place parameters before the template's own parameters.

Use `InsertParameter` in the `buildMethod` or `buildConstructor` callback of <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceMethod*> or <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceConstructor*>.

### Example: inserting parameters before template parameters

The following example introduces a `Greet` method. The template defines a `greeting` parameter with a default value. The aspect inserts `firstName` and `lastName` parameters before the template parameter using `InsertParameter`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/InsertParameter.cs name="Inserting parameters"]

> [!div class="see-also"]
> <xref:dependency-injection>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*>
> <xref:Metalama.Framework.Advising.PullStrategy>
> <xref:Metalama.Framework.Advising.IPullStrategy>
> <xref:Metalama.Framework.Advising.IConstructorOverloadingStrategy>
> <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy>
> <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBaseBuilder.InsertParameter*>
