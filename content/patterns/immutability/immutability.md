---
uid: immutability
summary: "Learn how to use Metalama.Patterns.Immutability to mark types as immutable and enforce immutability constraints in your C# code."
level: 200
keywords: "immutability, immutable type, C#, Metalama.Patterns.Immutability, ImmutableAttribute, ConfigureImmutability, shallow immutability, deep immutability, Metalama"
created-date: 2024-06-11
modified-date: 2025-11-30
---

# Metalama.Patterns.Immutability

Immutability is a widely recognized concept in software programming. An _immutable type_ is a type whose instances can't be modified after creation. Designs that use immutable types are typically easier to understand than those that rely heavily on mutating objects. Examples of immutable types in C# include intrinsic types like `int`, `float`, or `string`, enums, delegates, most system value types like `DateTime`, and collections from the `System.Collections.Immutable` namespace.

The `Metalama.Patterns.Immutability` package provides the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect and the <xref:Metalama.Patterns.Immutability.Configuration.ImmutabilityConfigurationExtensions.ConfigureImmutability*> fabric method to mark and enforce immutability.

## Benefits

The `Metalama.Patterns.Immutability` package provides the following benefits:

* **Exposes immutability to other aspects**: Provides immutability information for code analysis by other aspects, such as Observable (see <xref:observability>).
* **Documents design intent**: Declares immutability explicitly in code, eliminating the need to infer it from implementation details.
* **Enforces immutability**: Reports warnings when a type marked as immutable contains mutable fields.

## Kinds of immutability

The `Metalama.Patterns.Immutability` package recognizes two kinds of immutability, represented by the <xref:Metalama.Patterns.Immutability.ImmutabilityKind> type:

* **Shallow immutability**: All instance fields are read-only and no automatic property has a setter.
* **Deep immutability**: All instance fields and automatic properties are of a deeply immutable type, applied recursively.

Deep immutability ensures that all objects reachable by recursively evaluating fields or properties are immutable. This provides guarantees for code analyses, such as those performed by the `Metalama.Patterns.Observability` package.

## System-supported types

The `Metalama.Patterns.Immutability` package contains rules that define the following types as deeply immutable:

* Intrinsic types like `bool`, `byte`, `int`, or `string`
* Structs from the `System` namespace
* Delegates and enums
* Immutable collections from the `System.Collections.Immutable` namespace, when all type parameters are themselves deeply immutable

Additionally, the following types are implicitly classified as shallowly immutable:

* Read-only structs
* Immutable collections from the `System.Collections.Immutable` namespace, when any type parameter isn't deeply immutable

> [!WARNING]
> The `Metalama.Patterns.Immutability` package doesn't attempt to infer the immutability of types by analyzing their fields. It relies purely on the rules defined above and the types manually marked as immutable.

## Marking types as immutable in source code

Mark a type as immutable by applying the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect. By default, the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> attribute represents shallow immutability. To represent deep immutability, supply the `ImmutabilityKind.Deep` argument.

The <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect reports warnings when fields aren't read-only or when automatic properties have a setter. Resolve the warning or suppress it using `#pragma warning disable`.

The `[Immutable]` aspect is automatically inherited by derived types.

To add this aspect in bulk, use a fabric method and <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>. For details, see <xref:fabrics-adding-aspects>.

### Example: Shallow immutability with warning

The following example shows a class marked as immutable that contains a mutable property. A warning is reported on this property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Immutability/Warning.cs diff-side="source"]

## Marking types you don't own

To assign an <xref:Metalama.Patterns.Immutability.ImmutabilityKind> to types where you can't add the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect, use the <xref:Metalama.Patterns.Immutability.Configuration.ImmutabilityConfigurationExtensions.ConfigureImmutability*> fabric extension method. Pass either an <xref:Metalama.Patterns.Immutability.ImmutabilityKind> if the type always has the same immutability, or an <xref:Metalama.Patterns.Immutability.Configuration.IImmutabilityClassifier> to determine the <xref:Metalama.Patterns.Immutability.ImmutabilityKind> dynamically. This mechanism is useful for generic types when their immutability depends on the immutability of type arguments.

### Example: Marking System.Uri as immutable

The following example marks the `Uri` class as deeply immutable. As a result, you can use a `Uri` property in the deeply immutable type `Person` without generating a warning.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Immutability/Fabric.cs  diff-side="source"]

> [!div class="see-also"]
> <xref:patterns>
> <xref:observability>
> <xref:caching-value-adapters>
