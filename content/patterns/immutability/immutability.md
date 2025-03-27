---
uid: immutability
level: 200
keywords: "immutability, immutable type, C#, Metalama.Patterns.Immutability, ImmutableAttribute, ConfigureImmutability, shallow immutability, deep immutability, Metalama"
---

# Metalama.Patterns.Immutability

Immutability is a widely recognized and beneficial concept in software programming. An immutable type refers to a type whose instances cannot be modified once they have been created. Designs that prioritize immutable types are typically easier to understand than those that heavily rely on mutating objects. Examples of immutable types in C# include intrinsic types like `int`, `float`, or `string`, enums, delegates, most system value types like `DateTime`, and collections from the `System.Collections.Immutable` namespace.

Metalama implements the Immutable Type concept in the `Metalama.Patterns.Immutability` package, specifically through the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect and the <xref:Metalama.Patterns.Immutability.Configuration.ImmutabilityConfigurationExtensions.ConfigureImmutability*> fabric method.

This package serves three purposes:

* It exposes the immutability of a type to other aspects, such as Observable (see <xref:observability>), to facilitate code analysis.
* It represents the design intent visibly, enhancing code readability by eliminating the need for the reader to infer the immutability from the type implementation.
* It reports warnings if a type marked as immutable contains mutable fields.


## Kinds of immutability

The `Metalama.Patterns.Immutability` package recognizes two kinds of immutability, represented by the <xref:Metalama.Patterns.Immutability.ImmutabilityKind> type:

* _Shallow_ immutability implies that all instance fields are _read-only_ and that no automatic property has a setter.
* _Deep_ immutability requires, recursively, that all instance fields and automatic properties are of a deeply immutable type.

Deep immutability ensures that all objects, reachable by recursively evaluating the fields or properties, are immutable. This provides guarantees to some code analyses, such as the one performed by the `Metalama.Patterns.Observability` package.

## System-supported types

The `Metalama.Patterns.Immutability` package contains rules that define the following types as _deeply immutable_:

* Intrinsic types like `bool`, `byte`, `int`, or `string`.
* Structs from the `System` namespace.
* Delegates and enums.
* Immutable collections from the `System.Collections.Immutable` namespace, when all type parameters are themselves deeply immutable.

Additionally, the following types are implicitly classified as _shallowly immutable_:
* Read-only structs.
* Immutable collections from the `System.Collections.Immutable` namespace, when any type parameter is not deeply immutable.

> [!WARNING]
> The `Metalama.Patterns.Immutability` package does _not_ attempt to infer the immutability of types by analyzing their fields. It relies purely on the rules defined above and the types manually marked as immutable, as described below.

## Marking types as immutable in source code

If you own the source code of a type, you can mark it as immutable by applying the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect to it. By default, the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> attribute represents _shallow_ immutability. To represent deep immutability, supply the `ImmutabilityKind.Deep` argument.

The <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect reports warnings when fields are not read-only or when automatic properties have a setter. You can either resolve the warning or ignore it using `#pragma warning disable`.

Note that the `Immutable` aspect is automatically inherited by derived types.

You can use a fabric method and <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*> to add this aspect in bulk. See <xref:fabrics-adding-aspects> for details.

### Example: Shallow immutability with warning

The following example shows a class marked as immutable, but containing a mutable property. A warning is reported on this property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Immutability/Warning.cs diff-side="source"]

## Marking types for which you don't own the source code

To assign an <xref:Metalama.Patterns.Immutability.ImmutabilityKind> to types to which you cannot add the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect, you can use the <xref:Metalama.Patterns.Immutability.Configuration.ImmutabilityConfigurationExtensions.ConfigureImmutability*> fabric extension method. You can pass either an <xref:Metalama.Patterns.Immutability.ImmutabilityKind>, if the type always has the same <xref:Metalama.Patterns.Immutability.ImmutabilityKind>, or an <xref:Metalama.Patterns.Immutability.Configuration.IImmutabilityClassifier> if you want to determine the <xref:Metalama.Patterns.Immutability.ImmutabilityKind> dynamically. This mechanism is useful in generic types when their immutability depends on the immutability of type arguments.

### Example: Marking System.Uri as immutable

The following example marks the `Uri` class as deeply immutable. Thanks to this, a `Uri` property can legally be used in the deeply immutable type `Person`, and no warning is reported.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Immutability/Fabric.cs  diff-side="source"]


