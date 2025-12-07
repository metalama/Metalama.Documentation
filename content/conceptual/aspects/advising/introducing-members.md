---
uid: introducing-members
level: 300
summary: "The document provides a comprehensive guide on how to add new members to an existing type using the Metalama Framework. It covers both declarative and programmatic methods and includes instructions for overriding existing implementations and referencing introduced members."
keywords:
  - Metalama Framework
  - add new members
  - declarative methods
  - programmatic methods
  - overriding implementations
  - referencing members
  - IntroduceAttribute
  - TemplateAttribute
  - IAspect
  - AdviserExtensions
  - introduce member
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Introducing members

In previous articles, you learned how to override existing type members. This article explains how to add new members to a type.

You can add the following types of members:

- Methods
- Constructors
- Fields
- Properties
- Events
- Operators
- Conversions

## Introducing members declaratively

The simplest way to introduce a member from an aspect is to implement it in the aspect and annotate it with the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> custom attribute, which has these notable properties:

| Property | Description |
|----------|-------------|
| <xref:Metalama.Framework.Aspects.TemplateAttribute.Name> | Sets the name of the introduced member. If not specified, the name of the introduced member is the name of the template itself. |
| <xref:Metalama.Framework.Aspects.IntroduceAttribute.Scope> | Determines whether the introduced member will be `static`. See <xref:Metalama.Framework.Aspects.IntroductionScope> for possible strategies. By default, copies from the template, except when you apply the aspect to a static member, which always makes the introduced member `static`. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.Accessibility> | Determines the member's accessibility (`private`, `protected`, `public`, etc.). By default, copies the template's accessibility. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.IsVirtual> | Determines whether the member will be `virtual`. By default, copies the template's characteristic. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.IsSealed> | Determines whether the member will be `sealed`. By default, copies the template's characteristic. |

> [!NOTE]
> Constructors can't be introduced declaratively.

### Example: ToString

This example shows an aspect that implements the `ToString` method, returning a string with the object type and a unique identifier.

This aspect replaces any hand-written implementation of `ToString`, which isn't desirable. To avoid this, introduce the method programmatically and conditionally.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceMethod.cs name="ToString"]

## Introducing members programmatically

The main limitation of declarative introductions is that you must know the name, type, and signature of the introduced member upfront—they can't depend on the aspect target. The programmatic approach lets your aspect fully customize the declaration based on the target code.

There are two steps to introduce a member programmatically:

### Step 1. Implement the template

Implement the template in your aspect class and annotate it with the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute. The template doesn't need the final signature.

### Step 2. Invoke AdviserExtensions.Introduce*

In your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, call one of the following methods and store the return value in a variable:

- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceMethod*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBuilder>
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceProperty*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IPropertyBuilder>
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceEvent*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IEventBuilder>
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IFieldBuilder>
- <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceConstructor*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IConstructorBuilder>

These methods create a member with the same characteristics as the template (name, signature, etc.), accounting for the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute properties.

To modify the name and signature of the introduced declaration, use the `buildMethod`, `buildProperty`, `buildEvent`, `buildField`, or `buildConstructor` parameter of the `Introduce*` method.

### Example: Update method

The following aspect introduces an `Update` method that assigns all writable fields in the target type. The method signature is dynamic: there is one parameter per writable field or property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/UpdateMethod.cs name="Update method"]

### Introducing a partial or abstract member

You can use any `Introduce*` method to add a `partial` or `abstract` member. However, the _template_ itself can't be `partial` or `extern` because that wouldn't be valid C#.

There are two ways to make a member `partial` or `abstract`:

- Set the `IsPartial` or `IsAbstract` property of the `[Template]` attribute.
- Set the `IsPartial` or `IsAbstract` property of the <xref:Metalama.Framework.Code.DeclarationBuilders.IMemberBuilder> object.

The implementation body of the template will be ignored if you set the `IsAbstract` or `IsPartial` property, so any implementation will do. However, if you don't want to have _any_ body, you can use the `extern` keyword on the template member. This keyword will be removed during compilation, and dummy implementations will be provided.

## Overriding existing implementations

### Specifying the override strategy

When you introduce a member to a type, the same member might already exist in that type or a parent type. The default strategy reports an error and fails the build. Change this behavior by setting the <xref:Metalama.Framework.Aspects.OverrideStrategy> for the advice:

- For declarative advice, set the <xref:Metalama.Framework.Aspects.IntroduceAttribute.WhenExists> property of the custom attribute.
- For programmatic advice, set the _whenExists_ optional parameter of the advice factory method.

### Accessing the overridden declaration

When you override a method, you'll usually want to invoke the base implementation. The same applies to properties and events. In plain C#, you use the `base` prefix. Metalama uses a similar approach.

- To invoke the base method or accessor with exactly the same arguments, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>.
- To invoke the base method with different arguments, use <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*?text=meta.Target.Method.Invoke>.
- To call the base property getter or setter, use <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Property.Value>.
- To access the base event, use <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Add*?text=meta.Event.Add>, <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Remove*?text=meta.Event.Remove> or <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Raise*?text=meta.Event.Raise>.

For details, see <xref:invokers>.

> [!NOTE]
> Inside an override template, invokers resolve to the _previous implementation layer_ by default—that is, the implementation before the current aspect, or the `base` implementation if you're the first aspect in the chain. In other contexts, invokers resolve to the _final layer_, which includes all aspect transformations and uses virtual dispatch when applicable. To specify a different layer, use the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*> method with the appropriate <xref:Metalama.Framework.Code.Invokers.InvokerOptions> value.

## Referencing introduced members in a template

When you introduce a member to a type, you'll often want to access it from templates. You can do this three ways:

### Option 1. Access the aspect template member

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged1.cs name="Introduce OnPropertyChanged"]

### Option 2. Use `meta.This` and write dynamic code

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged3.cs name="Introduce OnPropertyChanged"]

### Option 3. Use the invoker of the builder object

If neither approach above offers the required flexibility (typically because the name of the introduced member is dynamic), use the invokers exposed on the builder object returned from the advice factory method.

> [!NOTE]
> Declarations introduced by an aspect or aspect layer aren't visible in the `meta` code model exposed to the same aspect or aspect layer. You must reference them differently. For details, see <xref:sharing-state-with-advice>.

For more details, see <xref:Metalama.Framework.Code.Invokers>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged2.cs name="Introduce OnPropertyChanged"]


### Example: Dirty tracking

The following example shows a `DirtyTracking` aspect that introduces an `IsDirty` property and a virtual `OnPropertyChanged` method. The aspect uses `WhenExists = OverrideStrategy.Override` so it can override an existing `OnPropertyChanged` in a base class while also introducing the method when no base exists.

> [!NOTE]
> An optimal dirty-tracking implementation would automatically instrument property setters to call `OnPropertyChanged`. This example assumes you don't own the properties—for instance, they might be in a base class you can't modify—so the aspect only introduces the `OnPropertyChanged` hook and relies on existing code to call it. For a complete change-tracking implementation that instruments properties automatically, see [Change Tracking](https://samples.metalama.net/change-tracking).

- `Entity` is a base class without the aspect but with its own `OnPropertyChanged` implementation.
- `Customer` derives from `Entity` and has the aspect. The aspect overrides `OnPropertyChanged` and calls `base.OnPropertyChanged`.
- `StandaloneOrder` has the aspect but no base class. The aspect introduces `OnPropertyChanged` as a new virtual method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DirtyTracking.cs name="Dirty tracking"]


## Referencing introduced members from source code

If you want source code (not aspect code) to reference declarations introduced by your aspect, users must make the target types `partial`. Without this keyword, introduced declarations won't be visible at design time in syntax completion, and the IDE will report errors.

The compiler won't complain because Metalama handles it, but the IDE will because it doesn't know about Metalama. Your aspect must follow standard C# compiler rules. Neither aspect authors nor Metalama can work around this limitation.

If the user doesn't add the `partial` keyword, Metalama will report a warning and offer a code fix.

> [!div class="see-also"]
> <xref:initializers>
> <xref:Metalama.Framework.Aspects.IntroduceAttribute>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceMethod*>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceProperty*>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*>
> <xref:Metalama.Framework.Aspects.OverrideStrategy>
> <xref:introducing-types>
