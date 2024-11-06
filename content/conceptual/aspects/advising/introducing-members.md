---
uid: introducing-members
level: 300
summary: "The document provides a comprehensive guide on how to add new members to an existing type using the Metalama Framework. It covers both declarative and programmatic methods and includes instructions for overriding existing implementations and referencing introduced members."
keywords: "Metalama Framework, add new members, declarative methods, programmatic methods, overriding implementations, referencing members, IntroduceAttribute, TemplateAttribute, IAspect, AdviserExtensions, introduce member"
created-date: 2023-02-17
modified-date: 2024-11-06
---

# Introducing members

In previous articles, you learned how to override the implementation of existing type members. This article will teach you how to add new members to an existing type.

Currently, you can add the following types of members:

- Methods
- Constructors
- Fields
- Properties
- Events
- Operators
- Conversions

## Introducing members declaratively

The simplest way to introduce a member from an aspect is to implement this member in the aspect and annotate it with the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> custom attribute. This custom attribute has the following notable properties:

| Property | Description |
|----------|-------------|
| <xref:Metalama.Framework.Aspects.TemplateAttribute.Name> | Sets the name of the introduced member. If not specified, the name of the introduced member is the name of the template itself. |
| <xref:Metalama.Framework.Aspects.IntroduceAttribute.Scope> | Determines whether the introduced member will be `static` or not. See <xref:Metalama.Framework.Aspects.IntroductionScope> for possible strategies. By default, it is copied from the template, except when the aspect is applied to a static member, in which case the introduced member is always `static`. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.Accessibility> | Determines if the member will be `private`, `protected`, `public`, etc. By default, the accessibility of the template is copied. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.IsVirtual> | Determines if the member will be `virtual`. By default, the characteristic of the template is copied. |
| <xref:Metalama.Framework.Aspects.TemplateAttribute.IsSealed> | Determines if the member will be `sealed`. By default, the characteristic of the template is copied. |

> [!NOTE]
> Constructors cannot be introduced declaratively.

### Example: ToString

The following example demonstrates an aspect that implements the `ToString` method. It will return a string that includes the object type and a reasonably unique identifier for that object.

Please note that this aspect will replace any hand-written implementation of `ToString`, which is not desirable. Currently, this can only be avoided by introducing the method programmatically and conditionally.

[comment]: # (TODO: #28807)

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceMethod.cs name="ToString"]

## Introducing members programmatically

The main limitation of declarative introductions is that the name, type, and signature of the introduced member must be known upfront. They cannot depend on the aspect target. The programmatic approach allows your aspect to fully customize the declaration based on the target code.

There are two steps to introduce a member programmatically:

### Step 1. Implement the template

Implement the template in your aspect class and annotate it with the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute. The template does not need to have the final signature.

### Step 2. Invoke AdviserExtensions.Introduce*

In your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, call one of the following methods and store the return value in a variable:

- <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceMethod*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBuilder>
- <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceProperty*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IPropertyBuilder>
- <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceEvent*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IEventBuilder>
- <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceField*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IFieldBuilder>
- <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceConstructor*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IConstructorBuilder>

A call to one of these methods creates a member by default that has the same characteristics as the template (name, signature, etc.), taking into account the properties of the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute.

To modify the name and signature of the introduced declaration, use the `buildMethod`, `buildProperty`, `buildEvent`, or `buildField` parameter of the `Introduce*` method.

### Example: Update method

The following aspect introduces an `Update` method that assigns all writable fields in the target type. The method signature is dynamic: there is one parameter per writable field or property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/UpdateMethod.cs name="Update method"]

### Introducing a partial or abstract member

You can use any of the `Introduce*` methods to add a `partial` or `abstract` member. However, the _template_ itself can be neither `partial` nor `extern` because that would not be valid C#.

There are two ways to make a member `partial` or `abstract`:

* Set the `IsPartial` or `IsAbstract` property of the `[Template]` attribute.
* Set the `IsPartial` or `IsAbstract` property of the <xref:Metalama.Framework.Code.DeclarationBuilders.IMemberBuilder> object.

The implementation body of the template will be ignored if you set the `IsAbstract` or `IsPartial` property, so any implementation will do. However, if you do not want to have _any_ body, you can use the `extern` keyword on the template member. This keyword will be removed during compilation, and dummy implementations will be provided.


## Overriding existing implementations

### Specifying the override strategy

When you want to introduce a member to a type, it may happen that the same member is already defined in this type or in a parent type. The default strategy of the aspect framework in this case is simply to report an error and fail the build. You can change this behavior by setting the <xref:Metalama.Framework.Aspects.OverrideStrategy> for this advice:

- For declarative advice, set the <xref:Metalama.Framework.Aspects.IntroduceAttribute.WhenExists> property of the custom attribute.
- For programmatic advice, set the _whenExists_ optional parameter of the advice factory method.

[comment]: # (TODO: The implementation and documentation are not final. Another property and parameter should be defined to cope with the case when the member is inherited.)

### Accessing the overridden declaration

Most of the time, when you override a method, you will want to invoke the base implementation. The same applies to properties and events. In plain C#, when you override a base-class member in a derived class, you call the member with the `base` prefix. A similar approach exists in Metalama.

- To invoke the base method or accessor with exactly the same arguments, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>.
- To invoke the base method with different arguments, use <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*?text=meta.Target.Method.Invoke>.
- To call the base property getter or setter, use <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Property.Value>.
- To access the base event, use <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Add*?text=meta.Event.Add>, <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Remove*?text=meta.Event.Remove> or <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Raise*?text=meta.Event.Raise>.

[comment]: # (TODO: When it will work, Disposable example.)

## Referencing introduced members in a template

When you introduce a member to a type, you will often want to access it from templates. There are three ways to do it:

### Option 1. Access the aspect template member

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged1.cs name="Introduce OnPropertyChanged"]

### Option 2. Use `meta.This` and write dynamic code

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged3.cs name="Introduce OnPropertyChanged"]

### Option 3. Use the invoker of the builder object

If neither of the approaches above offers you the required flexibility (typically because the name of the introduced member is dynamic), use the invokers exposed on the builder object returned from the advice factory method.

> [!NOTE]
> Declarations introduced by an aspect or aspect layer are not visible in the `meta` code model exposed to the same aspect or aspect layer. To reference builders, you have to reference them differently. For details, see <xref:sharing-state-with-advice>.

For more details, see <xref:Metalama.Framework.Code.Invokers>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged2.cs name="Introduce OnPropertyChanged"]

## Referencing introduced members from source code

If you want the _source_ code (not your aspect code) to reference declarations introduced by your aspect, the _user_ of your aspect needs to make the target types `partial`. Without this keyword, the introduced declarations will not be visible at design time in syntax completion, and the IDE will report errors. Note that the _compiler_ will not complain because Metalama replaces the compiler, but the IDE will because it does not know about Metalama, and therefore your aspect has to follow the rules of the C# compiler. However inconvenient it may be, there is nothing you as an aspect author, or us as the authors of Metalama, can do.

If the user does not add the `partial` keyword, Metalama will report a warning and offer a code fix.

> [!NOTE]
> In __test projects__ built using `Metalama.Testing.AspectTesting`, the Metalama compiler is _not_ activated. Therefore, the source code of test projects cannot reference introduced declarations. Since the present documentation relies on `Metalama.Testing.AspectTesting` for all examples, we cannot include an example here.

