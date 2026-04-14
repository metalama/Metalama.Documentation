---
uid: introducing-constructor-parameters
level: 400
summary: "This article explains how to introduce parameters to constructors, including source-compatible parameters with default values and binary-compatible parameters with forwarding constructors."
keywords: "IntroduceParameter, constructor parameter, dependency injection, IConstructor, AdviserExtensions, default value, pullStrategy, source-compatibility constructor, ForwardSourceConstructors, ConstructorOverloadingStrategy, binary compatibility, source compatibility"
created-date: 2023-02-20
modified-date: 2026-04-13
---

# Introducing constructor parameters

Most of the time, an aspect requires introducing a parameter to a constructor when it needs to retrieve a dependency from a dependency injection framework. In such situations, use the <xref:Metalama.Extensions.DependencyInjection> framework, as detailed in <xref:dependency-injection>.

Typically, implementations of dependency injection frameworks introduce parameters using the method outlined here.

## Source-compatible parameters (with a default value)

To append a parameter with a compile-time constant default value, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that accepts a `defaultValue` argument. This method requires the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value.

Because the parameter has a default value, existing callers can omit the new argument, so **source compatibility** is preserved automatically — no forwarding constructor is needed. However, the default value must be a compile-time constant (a <xref:Metalama.Framework.Code.TypedConstant>).

The `pullStrategy` parameter allows you to specify the value passed to this parameter in other constructors that call the specified constructor, using the `: this(...)` or `: base(...)` syntax. This parameter accepts an <xref:Metalama.Framework.Advising.IPullStrategy> implementation. To create a pull strategy, use one of the factory methods of the <xref:Metalama.Framework.Advising.PullStrategy> class, such as <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> or <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*>.

### Example: source-compatible parameter

The example below demonstrates an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and invokes the `IInstanceRegistry.Register(this)` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Source-compatible parameter with default value"]

## Binary-compatible parameters (with a forwarding constructor)

To append a parameter whose default value can be any expression (not just a compile-time constant), use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that does _not_ accept a `defaultValue` argument.

Metalama will generate a _forwarding constructor_ to preserve both **source and binary compatibility**: a constructor with the pre-mutation signature that chains via `: this(...)` to the mutated constructor. This constructor is marked with <xref:Metalama.Framework.RunTime.SourceCompatibilityConstructorAttribute>.

The value passed by the forwarding constructor to the augmented constructor comes from the <xref:Metalama.Framework.Advising.IPullStrategy> parameter of the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> method. Use <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> to supply an expression (such as `DateTime.Now` or a factory method call) for the parameter value in the forwarding constructor. Unlike the source-compatible approach, this expression does not need to be a compile-time constant.

For more advanced scenarios, implement <xref:Metalama.Framework.Advising.IPullStrategy> directly. Your implementation can detect whether it is being called for a forwarding constructor by using the <xref:Metalama.Framework.Code.ConstructorExtensions.IsSourceCompatibilityConstructor*> extension method.

### Overloading strategy

The `overloadingStrategy` parameter controls whether and how forwarding constructors are generated. It accepts an <xref:Metalama.Framework.Advising.IConstructorOverloadingStrategy> implementation.

The <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy> class provides two built-in strategies:

| Strategy | Description |
|----------|-------------|
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardSourceConstructors> | Generates a forwarding constructor for every source constructor that the framework mutates. This is the default when `overloadingStrategy` is `null`. |
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardDefaultConstructor> | Generates a forwarding constructor only when the mutated constructor is the parameterless constructor. This is useful for types that must remain constructible via `Activator.CreateInstance<T>()` or a `new()` generic constraint. |

Both strategies return a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy> that exposes a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy.WithObsoleteAttribute*> method. Use this method to decorate the generated forwarding constructor with `[Obsolete]`, signaling to downstream callers that they should migrate to the new constructor signature.

### Example: binary-compatible parameter with forwarding constructors

The following example demonstrates an aspect that introduces a `DateTime creationTime` parameter to all constructors. The framework generates forwarding constructors that supply `DateTime.Now` as the default value, preserving binary compatibility.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceRequiredParameter.cs name="Binary-compatible parameter with forwarding constructors"]

## Parameters on record primary constructors

> [!NOTE]
> When `IntroduceParameter` targets a record's primary constructor, the introduced parameter is not materialized as part of the record's value shape by default. This means the parameter will not generate an auto-property, will not appear in `Deconstruct`, and will not participate in `Equals`, `GetHashCode`, or `ToString`. This prevents accidental pollution of a record's identity with infrastructure parameters (such as DI dependencies or contextual objects).
>
> To materialize the parameter as part of the record's value shape, explicitly opt in by using `PullStrategy.IntroduceParameterAndPull(materializeOnRecord: true)`.

> [!div class="see-also"]
> <xref:dependency-injection>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*>
> <xref:Metalama.Framework.Advising.PullStrategy>
> <xref:Metalama.Framework.Advising.IPullStrategy>
> <xref:Metalama.Framework.Advising.IConstructorOverloadingStrategy>
> <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy>
