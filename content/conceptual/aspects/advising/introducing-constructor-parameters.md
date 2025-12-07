---
uid: introducing-constructor-parameters
level: 400
summary: "The document explains how to introduce a parameter to a constructor using the Metalama.Extensions.DependencyInjection framework, specifically the AdviserExtensions.IntroduceParameter method. It also provides a relevant example."
keywords: "IntroduceParameter, constructor parameter, dependency injection, Metalama.Extensions.DependencyInjection, IConstructor, AdviserExtensions, default value, pullStrategy, IInstanceRegistry, Register method"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Introducing constructor parameters

Most of the time, an aspect requires introducing a parameter to a constructor when it needs to retrieve a dependency from a dependency injection framework. In such situations, use the <xref:Metalama.Extensions.DependencyInjection> framework, as detailed in <xref:dependency-injection>.

Typically, implementations of dependency injection frameworks introduce parameters using the method outlined here.

To append a parameter to a constructor, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> method. This method requires several arguments: the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value for this parameter. A parameter can't be introduced without specifying a default value.

The `pullStrategy` parameter of the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> method allows you to specify the value passed to this parameter in other constructors that call the specified constructor, using the `: this(...)` or `: base(...)` syntax. This parameter accepts an <xref:Metalama.Framework.Advising.IPullStrategy> implementation. To create a pull strategy, use one of the factory methods of the <xref:Metalama.Framework.Advising.PullStrategy> class, such as <xref:Metalama.Framework.Advising.PullStrategy.UseExpression*> or <xref:Metalama.Framework.Advising.PullStrategy.IntroduceParameterAndPull*>.

## Example

The example below demonstrates an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and invokes the `IInstanceRegistry.Register(this)` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Introducing parameters"]

> [!div class="see-also"]
> <xref:dependency-injection>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*>
> <xref:Metalama.Framework.Advising.PullStrategy>
> <xref:Metalama.Framework.Advising.IPullStrategy>
