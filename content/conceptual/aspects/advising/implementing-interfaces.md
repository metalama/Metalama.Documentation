---
uid: implementing-interfaces
level: 300
summary: "The document provides a guide on how to implement interfaces using the programmatic advising API in the Metalama Framework, with examples for IDisposable and Deep cloning."
keywords: "Metalama Framework, implementing interfaces, programmatic advising API, AdviserExtensions.ImplementInterface, OverrideStrategy, InterfaceMemberAttribute"
created-date: 2023-02-17
modified-date: 2025-11-30
---
# Implementing interfaces

Certain aspects necessitate modifying the target type to implement a new interface. This can only be achieved by using the programmatic advising API.

## Step 1. Call AdviserExtensions.ImplementInterface

Within your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, invoke the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method.

You might need to pass a value to the <xref:Metalama.Framework.Aspects.OverrideStrategy> parameter to handle the situation where the target type, or any of its ancestors, already implements the interface. The most common behavior is `OverrideStrategy.Ignore`, but the default value is `OverrideStrategy.Fail`, consistent with other advice kinds.

> [!NOTE]
> Unlike in PostSharp, it isn't necessary in Metalama for the aspect class to implement the introduced interface.

## Step 2, Option A. Add interface members to the aspect class, declaratively

The next step is to ensure that the aspect class generates all interface members. We can do this declaratively or programmatically, and add implicit or explicit implementations.

> [!NOTE]
> The <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method doesn't verify if the aspect generates all required members. If your aspect fails to introduce a member, the C# compiler will report errors.

Let's start with the declarative approach.

Implement all interface members in the aspect and annotate them with the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute?text=[InterfaceMember]> custom attribute. This attribute instructs Metalama to introduce the member to the target class but _only_ if the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> succeeds. If the advice is ignored because the type already implements the interface and `OverrideStrategy.Ignore` has been used, the member will _not_ be introduced to the target type.

By default, an implicit (public) implementation is created. You can use the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute.IsExplicit> property to specify that an explicit implementation must be created instead of a public method.

> [!NOTE]
> Using the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> also works but isn't recommended in this case because this approach ignores the result of the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method.

## Example: IDisposable

In the subsequent example, the aspect introduces the `IDisposable` interface. The implementation of the `Dispose` method disposes of all fields or properties that implement the `IDisposable` interface.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Disposable.cs name="Disposable"]

## Example: Deep cloning

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DeepClone.cs name="Deep Clone"]

## Step 2, Option B. Add interface members to the aspect class, programmatically

This approach can be used instead or in addition to the declarative one.

It is useful in the following situations:

* when the introduced interface is unknown to the aspect's author, for example, when it can be dynamically specified by the aspect's user;
* when introducing a generic interface thanks to the ability to use generic templates (see <xref:template-parameters>).

To programmatically add interface members, use one of the `Introduce` methods of the <xref:Metalama.Framework.Aspects.AdviserExtensions> class, as explained in <xref:introducing-members>. Make sure that these members are public.

If instead of adding public members you need to add explicit implementations, use the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.ExplicitMembers> property of the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult> returned by the <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*> method, and call any of its `Introduce` methods.

## Referencing interface members in other templates

When introducing an interface member to the type, you often want to access it from templates. Unless the member is an explicit implementation, you have two options:

### Option 1. Access the aspect template member

```cs
this.Dispose();
```

### Option 2. Use `meta.This` and write dynamic code

```cs
meta.This.Dispose();
```

### Option 3. Use invokers

If the members have been added programmatically, you can use invoker APIs like <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*?text=IMethod.Invoke> for methods or <xref:Metalama.Framework.Code.IExpression.Value> for properties.

### Accessing explicit implementations

The following strategies can be employed to access explicit implementations:

* Cast the instance to the interface and access the member:

    ```cs
    ((IDisposable)meta.This).Dispose();
    ```

* Introduce a private method with the concrete method implementation, and call this private member both from the interface member and the templates.

> [!div class="see-also"]
> <xref:introducing-members>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*>
> <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute>
> <xref:Metalama.Framework.Aspects.OverrideStrategy>
