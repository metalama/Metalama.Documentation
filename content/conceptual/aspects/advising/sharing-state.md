---
uid: sharing-state-with-advice
level: 400
summary: "The document provides strategies for sharing compile-time state between different pieces of advice or the BuildAspect method and the advice. It discusses sharing state with compile-time template parameters, the Tags property, and the AspectState property."
keywords: "compile-time state, BuildAspect method, template parameters, Tags property, AspectState property, sharing state, advice methods, .NET, annotations, IAspectBuilder"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Sharing state with advice

When you need to share _compile-time_ state between different pieces of advice or between your implementation of the `BuildAspect` method and the advice, several strategies are available.

> [!IMPORTANT]
> **Aspects must be designed as immutable classes.** Never store target-specific state in aspect fields.
>
> In scenarios involving inherited aspects or cross-project validators, **the same aspect instance is reused across multiple target declarations**. Storing target-specific state in a field will cause incorrect behavior because that state will be shared across all targets.
>
> If you need to access target-specific information in a template, prefer computing it directly using `meta.Target`. This approach is not only simpler but also more efficient: templates are not executed at design time (in the IDE), so computations in templates don't add overhead to the design-time experience. If computing in the template is not possible, use one of the strategies described below.
>
> For more details, see <xref:aspect-design>.

> [!NOTE]
> This article is about sharing _compile-time_ state. If you need to share _run-time_ state with advice, a different strategy must be adopted. For instance, you could introduce a field in the target type and utilize it from several advice methods.

## Sharing state with compile-time template parameters

This is the most direct approach for passing values from your `BuildAspect` method to a template method. However, it is only applicable to method, constructor, or accessor templates.

For more details, see <xref:template-parameters>.

## Sharing state with tags

Compile-time template parameters are not available for event, property, or field templates. A straightforward alternative is to use tags, which are arbitrary name-value pairs.

The idea is to set tags in your `BuildAspect` method and to read them from the template implementation.

Tags can be represented as arbitrary objects (including anonymous objects) or as `IReadOnlyDictionary<string, object?>` objects. When the tags object does not readily implement the dictionary interface, an accessor implementing the `IReadOnlyDictionary<string, object?>` interface is created, giving access to the object properties through a dictionary.

For instance, the anonymous object `new { A = 5, B = "x", C = builder.Target.DeclaringType }` defines three tags arbitrarily named `A`, `B`, and `C`.

There are two ways to add tags from the `BuildAspect` method:

* by passing an argument to the `tags` parameter of the advice method, or
* by setting the <xref:Metalama.Framework.Aspects.IAspectBuilder.Tags?text=IAspectBuilder.Tags> property at any moment in the `BuildAspect` method (even after the advice method has been called).

When you use both ways at the same time, the tags will be merged into a single dictionary.

In your template implementation, you can read the tags by calling the <xref:Metalama.Framework.Aspects.meta.Tags?text=meta.Tags> API, which returns an <xref:Metalama.Framework.Aspects.IObjectReader>. This interface derives from `IReadOnlyDictionary<string, object?>`. For instance, you would use the `meta.Tags["A"]` expression to access the tag named `A` that you defined in the previous step.

The <xref:Metalama.Framework.Aspects.IObjectReader> interface has an additional property <xref:Metalama.Framework.Aspects.IObjectReader.Source> that exposes the original object, not flattened as a dictionary.

### Example: Tags passed as an argument

In the following example, the tags are set by passing an argument to the advice method. We use an anonymous type to set the value and the dictionary interface to read it.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Tags.cs name="Tags 1"]

### Example: Tags set as a property

In the following example, the tags are set by setting the <xref:Metalama.Framework.Aspects.IAspectBuilder.Tags?text=aspectBuilder.Tags> property. We defined a compile-time record to represent the tags in a strongly typed way and use the <xref:Metalama.Framework.Aspects.IObjectReader.Source?text=IObjectReader.Source> property to read the object.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Tags_Property.cs name="Tags 2"]

## Sharing state with the AspectState property

When you want to share state not from templates inside the current aspect instance, but with other aspect instances, you set the <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState?text=IAspectBuilder.AspectState> property. Its value is exposed for read-only access on the <xref:Metalama.Framework.Aspects.IAspectInstance.AspectState?text=IAspectInstance.AspectState> property. It is therefore visible to child aspects and aspects that inherit from them (i.e., successors) through the <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors> property.

This property is opaque to the Metalama framework. You can use it for any purpose, but at your own risk. You're responsible for thread safety if you choose to have any mutable state in your aspect state.

Objects assigned to <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState> must implement the <xref:Metalama.Framework.Aspects.IAspectState> interface, which makes them automatically serializable. This mechanism ensures that the aspect state is available in cross-project scenarios. For details, see <xref:aspect-serialization>.

## Sharing state with annotations

A last way to share state with successor aspects is to use annotations. Annotations are arbitrary objects attached to declarations. They are visible from every aspect. However, unlike aspect state, annotations are not serialized and are only visible within the current project.

You can add annotations from the `BuildAspect` method using the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddAnnotation*> advice method.

You can read annotations using `declaration.Enhancements().GetAnnotations<T>` where `T` is the type of your annotation (see <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetAnnotations*>).

> [!div class="see-also"]
> <xref:template-parameters>
> <xref:aspect-design>
> <xref:Metalama.Framework.Aspects.IAspectBuilder.Tags>
> <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState>
> <xref:aspect-serialization>
