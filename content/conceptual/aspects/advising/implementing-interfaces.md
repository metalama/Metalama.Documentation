---
uid: implementing-interfaces
level: 300
summary: "The document provides a guide on how to implement interfaces using the programmatic advising API in the Metalama Framework, with examples for IDisposable and Deep cloning."
keywords: "Metalama Framework, implementing interfaces, programmatic advising API, AdviserExtensions.ImplementInterface, OverrideStrategy, InterfaceMemberAttribute"
created-date: 2023-02-17
modified-date: 2025-11-30
---
# Implementing interfaces

Some aspects require modifying the target type to implement a new interface. This requires the programmatic advising API.

## Step 1. Call AdviserExtensions.ImplementInterface

Within your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, invoke the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method.

You might need to pass a value to the <xref:Metalama.Framework.Aspects.OverrideStrategy> parameter to handle the situation where the target type, or any of its ancestors, already implements the interface. The most common behavior is `OverrideStrategy.Ignore`, but the default value is `OverrideStrategy.Fail`, consistent with other advice kinds.

> [!NOTE]
> Unlike PostSharp, Metalama doesn't require the aspect class to implement the introduced interface.

## Step 2, Option A. Add interface members declaratively

The next step is to ensure that the aspect class generates all interface members. You can do this declaratively or programmatically, and add implicit or explicit implementations.

> [!NOTE]
> The <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method doesn't verify if the aspect generates all required members. If your aspect fails to introduce a member, the C# compiler will report errors.

Let's start with the declarative approach.

Implement all interface members in the aspect and annotate them with the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute?text=[InterfaceMember]> custom attribute. This attribute instructs Metalama to introduce the member to the target class, but _only_ if the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> succeeds. If the advice is ignored because the type already implements the interface and `OverrideStrategy.Ignore` has been used, the member will _not_ be introduced to the target type.

By default, Metalama creates an implicit (public) implementation. You can use the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute.IsExplicit> property to specify that an explicit implementation must be created instead of a public method.

> [!NOTE]
> Using the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> also works but isn't recommended in this case because this approach ignores the result of the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method.

## Example: IDisposable

This example shows an aspect that introduces the `IDisposable` interface. The implementation of the `Dispose` method disposes of all fields or properties that implement the `IDisposable` interface.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Disposable.cs name="Disposable"]

## Example: Deep cloning

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DeepClone.cs name="Deep Clone"]

## Step 2, Option B. Add interface members programmatically

Use this approach instead of or with the declarative approach when:

* The introduced interface is unknown to the aspect's author, for example, when the aspect's user can dynamically specify it.
* Introducing a generic interface by using generic templates (see <xref:template-parameters>).

To programmatically add interface members, use one of the `Introduce` methods of the <xref:Metalama.Framework.Aspects.AdviserExtensions> class, explained in <xref:introducing-members>. Ensure these members are public.

To add explicit implementations instead of public members, use the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.ExplicitMembers> property of the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult> returned by the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method, and call any of its `Introduce` methods.

## Implementing explicit members dynamically

When the interface to implement is unknown at design time, or when you need to generate explicit implementations programmatically, use the result of the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method.

The <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.ExplicitMembers> property provides an adviser for introducing explicit member implementations. Use its `IntroduceMethod`, `IntroduceProperty`, `IntroduceEvent`, or `IntroduceIndexer` methods to add implementations dynamically.

To iterate over interface members, access the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.Interfaces> collection, which contains an <xref:Metalama.Framework.Advising.IInterfaceImplementationResult> for each interface (including base interfaces). Each result provides:

* `InterfaceType` — the interface being implemented
* `Outcome` — whether the interface was implemented or ignored
* `ExplicitMembers` — an adviser for that specific interface

To get interface members for use in templates, retrieve them from the interface type:

```cs
var result = builder.ImplementInterface(typeof(IDisposable));
var disposeMethod = result.Interfaces[0].InterfaceType.Methods.Single(m => m.Name == "Dispose");
```

### Example: Delegate interface

This example demonstrates dynamic explicit interface implementation. The `DelegateInterface` aspect implements an interface by delegating all member calls to a field.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DelegateInterface.cs name="Delegate Interface"]

## Referencing interface members in other templates

When introducing an interface member to the type, you often want to access it from templates. There are four ways to call an introduced method from another template:

### Option 1. Access the aspect template member

Call the method directly on `this`. This is the simplest approach.

```cs
this.Dispose();
```

### Option 2. Use `meta.This` and write dynamic code

Use `meta.This` when you need dynamic dispatch or late binding.

```cs
meta.This.Dispose();
```

### Option 3. Use invokers

If you have an `IMethod` reference, use the invoker API to call it. For details, see <xref:invokers>.

You can obtain the `IMethod` from:

* The interface type using `TypeFactory.GetType(typeof(IDisposable)).Methods`
* The advice result using `builder.IntroduceMethod(...).Declaration`

```cs
disposeMethod.Invoke();
```

### Option 4. Cast to interface

Cast `meta.This` to the interface type. This is useful for explicit implementations and ensures the interface method is called.

```cs
((IDisposable) meta.This).Dispose();
```

### Example: Four ways to call interface members

This example demonstrates all four approaches to call an introduced `Dispose` method from another template:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ImplementInterfaceFourWays.cs name="Four ways to call interface members"]

> [!div class="see-also"]
> <xref:introducing-members>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*>
> <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult>
> <xref:Metalama.Framework.Advising.IInterfaceImplementationResult>
> <xref:Metalama.Framework.Advising.IInterfaceImplementationAdviser>
> <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute>
> <xref:Metalama.Framework.Aspects.OverrideStrategy>
