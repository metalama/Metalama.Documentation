---
uid: introducing-constructor-parameters
level: 400
summary: "This article explains how to introduce parameters to constructors, either by adding an optional parameter with a default value or by adding a forwarding constructor that preserves binary compatibility."
keywords: "IntroduceParameter, constructor parameter, dependency injection, IConstructor, AdviserExtensions, default value, pullStrategy, optional parameter, forwarding constructor, ForwardSourceConstructors, ConstructorOverloadingStrategy, binary compatibility, source compatibility"
created-date: 2023-02-20
modified-date: 2026-04-15
---

# Introducing constructor parameters

Most of the time, an aspect introduces a constructor parameter to retrieve a dependency from a dependency injection framework. In such situations, use the <xref:Metalama.Extensions.DependencyInjection> framework, as detailed in <xref:dependency-injection>; DI framework implementations themselves introduce parameters using the method outlined here.

The <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> advice supports two mechanisms. Both preserve source compatibility (existing source code that constructs the type keeps compiling), but they differ on other axes:

| | Adding an optional parameter | Adding a required parameter, pulled from a forwarding constructor |
|---|---|---|
| Default value | Must be a compile-time constant | Any expression (`DateTime.Now`, a factory call, …) |
| Binary compatibility | Not preserved (IL signature changes) | Preserved (pre-mutation signature retained) |
| Constructor count | Unchanged | One additional constructor per mutated constructor |

**Adding an optional parameter** is the right choice when the type is instantiated by a dependency-injection container and binary compatibility is not a concern. Keeping the constructor count unchanged avoids issues with reflection-based consumers that walk `Type.GetConstructors()`. Notably, [`Microsoft.Extensions.DependencyInjection.ActivatorUtilities`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.activatorutilities) (used throughout ASP.NET Core for typed controllers, hosted services, and framework factories) requires *exactly one* applicable constructor and throws when several have all parameters resolvable. Serializers, test fixtures, and object-graph builders each have their own constructor-selection rules and can similarly be disturbed by the extra overload.

**Adding a required parameter, pulled from a forwarding constructor** is the right choice when the type is part of a public API (binary compatibility matters), when the default value must be a non-constant expression, or when the parameterless constructor must remain callable, for instance because the type is instantiated via `Activator.CreateInstance(type)`, `Activator.CreateInstance<T>()`, or a `new()` generic constraint. The dedicated <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardDefaultConstructor> strategy preserves the parameterless constructor specifically for this scenario.

## Adding an optional parameter

To append a parameter with a compile-time constant default value, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that accepts a `defaultValue` argument. This method requires the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value.

Because the parameter has a default value, existing callers can omit the new argument, so **source compatibility** is preserved automatically, without needing a forwarding constructor. However, the default value must be a compile-time constant (a <xref:Metalama.Framework.Code.TypedConstant>).

The `pullStrategy` parameter allows you to specify the value passed to this parameter in other constructors that call the specified constructor, using the `: this(...)` or `: base(...)` syntax. This parameter accepts an <xref:Metalama.Framework.Advising.IPullStrategy> implementation. To create a pull strategy, use one of the factory methods of the <xref:Metalama.Framework.Advising.PullStrategy> class, such as <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> or <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*>.

The <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*> method accepts a `reuseExistingParameterOfCompatibleType` parameter. When set to `true`, if a constructor that calls the target constructor via `: this(...)` or `: base(...)` already has a parameter whose type is the same as or more specific than the type being introduced, the existing parameter is forwarded instead of adding a duplicate. If the existing parameter was previously introduced and has a less specific type, it is automatically replaced with the more specific type. This is particularly useful for dependency injection scenarios where two parameters of the same service type on a single constructor are never intentional.

### Example: optional parameter

The example below demonstrates an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and invokes the `IInstanceRegistry.Register(this)` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Source-compatible parameter with default value"]

## Adding a required parameter, pulled from a forwarding constructor

Use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> overload that does _not_ accept a `defaultValue` argument. The mechanism has two complementary parts:

1. **The new parameter is added as required** to the existing constructor, with no default value, so every call site must supply a value.

2. **A forwarding constructor preserves the pre-mutation signature.** It retains the old constructor's parameter list and chains to the mutated constructor via `: this(...)`, passing a value for the new parameter that is produced by an <xref:Metalama.Framework.Advising.IPullStrategy> (for example, <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> for an expression such as `DateTime.Now`, or <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*> for a dependency pulled from the DI container).

Together, these preserve both **source and binary compatibility**: external callers still bind to the original signature, now served by the forwarding constructor.

The forwarding constructor can also be marked with `[Obsolete]` by calling <xref:Metalama.Framework.Advising.ForwardConstructorStrategy.WithObsoleteAttribute*> on the overloading strategy. This signals to downstream callers that they should migrate from the original signature to the new one, while still compiling against the forwarder in the meantime (see [Overloading strategy](#overloading-strategy) below).

> [!NOTE]
> Generated code carries two marker attributes that make the transformation self-describing:
>
> - `[SourceCompatibilityConstructor]` (<xref:Metalama.Framework.RunTime.SourceCompatibilityConstructorAttribute>) is placed on each generated forwarding constructor, distinguishing Metalama-generated forwarders from constructors written by the user. You can check for it programmatically from a pull strategy by calling <xref:Metalama.Framework.Code.ConstructorExtensions.IsSourceCompatibilityConstructor*> on the target <xref:Metalama.Framework.Code.IConstructor>.
> - `[AspectGenerated]` (<xref:Metalama.Framework.RunTime.AspectGeneratedAttribute>) is placed on each introduced parameter when the target constructor is reachable from external assemblies. This lets Metalama (and other tools) reconstruct the pre-transformation identity of the constructor, which matters for cross-assembly scenarios such as re-applying an aspect to an already-transformed type.

The `IPullStrategy` is also consulted when user-written chained constructors (`: this(...)` or `: base(...)`) call the mutated constructor without supplying the new argument, so the pull mechanism is the single source of truth for the new parameter's value wherever it is needed.

For more advanced scenarios, implement <xref:Metalama.Framework.Advising.IPullStrategy> directly. Your implementation can detect whether it is being called for a forwarding constructor by using the <xref:Metalama.Framework.Code.ConstructorExtensions.IsSourceCompatibilityConstructor*> extension method.

### Overloading strategy

The `overloadingStrategy` parameter controls whether and how forwarding constructors are generated. It accepts an <xref:Metalama.Framework.Advising.IConstructorOverloadingStrategy> implementation.

The <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy> class provides two built-in strategies:

| Strategy | Description |
|----------|-------------|
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardSourceConstructors> | Generates a forwarding constructor for every source constructor that the framework mutates. This is the default when `overloadingStrategy` is `null`. |
| <xref:Metalama.Framework.Advising.ConstructorOverloadingStrategy.ForwardDefaultConstructor> | Generates a forwarding constructor only when the mutated constructor is the parameterless constructor. This is useful for types that must remain constructible via `Activator.CreateInstance<T>()` or a `new()` generic constraint. |

Both strategies return a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy> that exposes a <xref:Metalama.Framework.Advising.ForwardConstructorStrategy.WithObsoleteAttribute*> method. Use this method to decorate the generated forwarding constructor with `[Obsolete]`, signaling to downstream callers that they should migrate to the new constructor signature.

### Example: forwarding constructor

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
