---
uid: introducing-types
level: 300
keywords: "IntroduceClass, Metalama, nested class, non-nested class, IAdviser, INamespace, BuildAspect, introduce members, Builder pattern, introduce types"
---

# Introducing types

Many patterns require you to create new types. This is the case, for instance, with the Memento, Enum View-Model, or Builder patterns. You can do this by calling the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> or <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceInterface*> advice method from your `BuildAspect` implementation.

> [!NOTE]
> The current version of Metalama allows you to introduce classes and interfaces. Support for structs, delegates, and enums will be added in a future release.

## Introducing a nested class

To introduce a nested class, call the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> or <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceInterface*> method from an `IAdviser<INamedType>`. For instance, if you have a <xref:Metalama.Framework.Aspects.TypeAspect>, just call `aspectBuilder.IntroduceClass( "Foo" )`.

### Example: nested class

In the following example, the aspect introduces a nested class named `Factory`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass.cs name="Introducing a nested class"]

## Introducing a top-level class

To introduce a non-nested class, you must first get hold of an `IAdviser<INamespace>`. Here are a few strategies to get a namespace adviser from any <xref:Metalama.Framework.Advising.IAdviser`1> or <xref:Metalama.Framework.Aspects.IAspectBuilder`1>:

* If you have an `IAdviser<ICompilation>` or `IAspectBuilder<ICompilation>` and want to add a type to `My.Namespace`, call the `WithNamespace("My.Namespace")` extension method.
* If you don't have an `IAdviser<ICompilation>`, call `aspectBuilder.With(aspectBuilder.Target.Compilation)`, then call `WithNamespace`.
* To get an adviser for the _current_ namespace, call `aspectBuilder.With(aspectBuilder.Target.GetNamespace())`.
* To get an adviser for a _child_ of the current namespace, call `aspectBuilder.With(aspectBuilder.Target.GetNamespace()).WithChildNamespace("ChildNs")`.

Once you have an `IAdviser<INamespace>`, call the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> advice method.

### Example: top-level class

In the following example, the aspect introduces a class in the `Builders` child namespace of the target class's namespace.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceTopLevelClass.cs name="Introducing a top-level class"]

## Adding class modifiers, attributes, base class, and type parameters

By default, the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> method introduces a non-generic class with no modifiers or custom attributes, derived from `object`. To add modifiers, custom attributes, a base type, or type parameters, you must supply a delegate of type `Action<INamedTypeBuilder>` to the `buildType` parameter of the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> method. This delegate receives an <xref:Metalama.Framework.Code.DeclarationBuilders.INamedTypeBuilder>, which exposes the required APIs.

### Example: setting up the type

In the following aspect, we continue the nested type example, make it `public`, and set its base type to the `Builder` nested type of the base class, if any.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass.cs name="Introducing a nested class"]

## Adding class members

Once you introduce the type, the next step is to introduce members: constructors, methods, fields, properties, etc.

Introduced types work exactly like source-defined ones.

When you call <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*>, it returns an <xref:Metalama.Framework.Advising.IClassIntroductionAdviceResult>. This interface derives from `IAdviser<INamedType>`, which has familiar extension methods like <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceMethod*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceField*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceProperty*> and so on.

> [!NOTE]
> All programmatic techniques described in <xref:introducing-members> also work with introduced types through the `IAdviser<INamedType>` interface.

### Example: adding properties

The following aspect copies the properties of the source object into the introduced `Builder` type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass_Members.cs name="Introducing a nested class"]

## Adding implemented interfaces

To add interface implementations to an introduced type, use the <xref:Metalama.Framework.Advising.AdviserExtensions.ImplementInterface*> method as mentioned in <xref:implementing-interfaces>.

### Final example: the Builder pattern

Let's finish this article with a complete implementation of the `Builder` pattern, a few fragments of which were illustrated above.

The input code for this pattern is an anemic class with get-only automatic properties.

The Builder aspect generates the following artifacts:

* A `Builder` nested class with:
    * A public constructor accepting all required properties,
    * Writable properties corresponding to all automatic properties of the source class,
    * A `Build` method that instantiates the source type,
* A private constructor in the source class, called by the `Builder.Build` method.

Ideally, the aspect should also test that the source type does not have another constructor or any settable property, but this is skipped in this example.

A key element of the design in the aspect is the `PropertyMapping` record, which maps a property of the source type to the corresponding property in the `Builder` type, the corresponding constructor parameter in the `Builder` type, and the corresponding parameter in the source type. We build this list in the `BuildAspect` method.

We use the `aspectBuilder.Tags` property to share this list with the template implementations, which can then read it from `meta.Tags.Source`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Builder.cs name="The Builder pattern"]

> [!NOTE]
> For more about the Builder pattern, see <xref:sample-builder>.
