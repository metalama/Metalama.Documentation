---
uid: introducing-constructor-parameters
level: 400
summary: "The document explains how to introduce a parameter to a constructor using the Metalama.Extensions.DependencyInjection framework, specifically the AdviserExtensions.IntroduceParameter method. It also provides a relevant example."
keywords: "IntroduceParameter, constructor parameter, dependency injection, Metalama.Extensions.DependencyInjection, IConstructor, AdviserExtensions, default value, pullAction, IInstanceRegistry, Register method"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Introducing constructor parameters

Most of the time, an aspect requires introducing a parameter to a constructor when it needs to retrieve a dependency from a dependency injection framework. In such situations, it is advisable to utilize the <xref:Metalama.Extensions.DependencyInjection> framework, as detailed in <xref:dependency-injection>.

Typically, implementations of dependency injection frameworks introduce parameters using the method outlined here.

To append a parameter to a constructor, the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> method is used. This method requires several arguments: the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value for this parameter. It's important to note that a parameter cannot be introduced without specifying a default value.

The `pullAction` parameter of the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceParameter*> method allows you to specify the value passed to this parameter in other constructors that call the specified constructor, using the `: this(...)` or `: base(...)` syntax. The `pullAction` parameter must receive a function that returns a <xref:Metalama.Framework.Advising.PullAction> value. To create a <xref:Metalama.Framework.Advising.PullAction> value, one of three available static members of this type should be used, such as <xref:Metalama.Framework.Advising.PullAction.UseExistingParameter*>, <xref:Metalama.Framework.Advising.PullAction.UseExpression*>, or <xref:Metalama.Framework.Advising.PullAction.IntroduceParameterAndPull*>.

## Example

The example below demonstrates an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and invokes the `IInstanceRegistry.Register(this)` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Introducing parameters"]
