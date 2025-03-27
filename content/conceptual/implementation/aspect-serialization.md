---
uid: aspect-serialization
level: 400
keywords: "serialization, cross-project effect, Metalama, IAspect, deserialized, custom serializer, ICompileTimeSerializable, NonCompileTimeSerialized, ValueTypeSerializer, ImportSerializerAttribute"
created-date: 2024-11-06
modified-date: 2024-11-06
---

# Serialization of aspects and other compile-time classes

Metalama relies on _serialization_ to handle situations when an aspect or _cross-project effect_, i.e., when it affects not only the current project but also, transitively, _referencing_ projects.

This happens in the following scenarios:

* Inheritable aspects (see <xref:aspect-inheritance>): inheritable instances of <xref:Metalama.Framework.Aspects.IAspect>, but also, if defined, of their respective <xref:Metalama.Framework.Aspects.IAspectState>, are serialized.
* Reference validators (see <xref:aspect-validating>): implementations of <xref:Metalama.Extensions.Validation.BaseReferenceValidator> and, if you're using <xref:Metalama.Extensions.Architecture>, any <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate> are serialized.
* Hierarchical options of non-sealed declarations (see <xref:Metalama.Framework.Options.IHierarchicalOptions`1> and <xref:exposing-options>).
* Annotations on non-sealed declarations (see <xref:Metalama.Framework.Code.IAnnotation>).

When any aspect or fabric has some cross-project effect, the following process is executed:

* In the current project:
    * The objects are _serialized_ into a binary stream.
    * The binary stream is stored in a _managed resource_ in the current project.
* In all referenced projects:
    * The objects are _deserialized_ from the managed resource.

## How are objects serialized?

Metalama uses a custom serializer, which is implemented in the <xref:Metalama.Framework.Serialization> namespace and has a similar behavior as Microsoft's legacy `BinaryFormatter` serializer.

Unlike more familiar JSON or XML serializers, Metalama's serializer:

* supports cyclic graphs instead of just trees,
* serializes the inner object structure, i.e., private fields, instead of the public interface.

These characteristics allow the serialization process to happen almost transparently.

## System-defined serializable types

The following types are serializable by default:

* Primitive types: `bool`, `byte`, `char`, `short`, `int`, `long`, `ushort`, `sbyte`, `uint`, `ulong`, `float`, `double`, `decimal`, `double`.
* All `enum` types.
* Arrays of any supported type (including `object[]` arrays, as long as items are of a supported type).
* Common system types: <xref:System.DateTime>, <xref:System.TimeSpan>, <xref:System.Guid>, <xref:System.Globalization.CultureInfo>.
* Collection types: <xref:System.Collections.Generic.List`1>, <xref:System.Collections.Generic.Dictionary`2>.
* Immutable collection types: <xref:System.Collections.Immutable.ImmutableDictionary`2>, <xref:System.Collections.Immutable.ImmutableArray`1>, <xref:System.Collections.Immutable.ImmutableHashSet`1>.
* Metalama types: <xref:Metalama.Framework.Code.IRef`1>, <xref:Metalama.Framework.Code.SerializableDeclarationId>, <xref:Metalama.Framework.Code.SerializableTypeId>, <xref:Metalama.Framework.Code.TypedConstant>, <xref:Metalama.Framework.Options.IncrementalKeyedCollection`2>, <xref:Metalama.Framework.Options.IncrementalHashSet`1>.

> [!WARNING]
> Code model declarations (<xref:Metalama.Framework.Code.IDeclaration>) and types (<xref:Metalama.Framework.Code.IType>) are, by design, _NOT_ serializable. If you want to serialize a declaration, you must serialize a _reference_ to it, obtained through the <xref:Metalama.Framework.Code.IDeclaration.ToRef*> method. The deserialized reference must then be resolved in its new context using the <xref:Metalama.Framework.Code.RefExtensions.GetTarget*?text=IRef.GetTarget> extension method.

## Custom serializable types

Metalama automatically generates serializers for any type deriving from the <xref:Metalama.Framework.Serialization.ICompileTimeSerializable> interface. This includes any aspect, fabric, or class implementing <xref:Metalama.Framework.Aspects.IAspectState>, <xref:Metalama.Framework.Code.IAnnotation>, <xref:Metalama.Framework.Options.IHierarchicalOptions>, <xref:Metalama.Extensions.Validation.BaseReferenceValidator>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate>.

You normally don't need to worry about the serialization process since it should usually work transparently. However, here are a few tricks to cope with corner cases:

### Skipping a field or property

To waive a field or automatic property from being serialized, annotate it with the <xref:Metalama.Framework.Serialization.NonCompileTimeSerializedAttribute?text=[NonCompileTimeSerialized]> attribute.

### Overriding the serializer when you own the type

If you can edit the source code of the class, you can override the default serializer by adding a nested class called `Serializer` and implementing the <xref:Metalama.Framework.Serialization.ValueTypeSerializer`1> or <xref:Metalama.Framework.Serialization.ReferenceTypeSerializer`1> class. Your nested class must have a default public constructor.

### Implementing a serializer for a third-party type

If you must implement serialization for a class whose source code you don't own (or to which you don't want to add a package reference to Metalama), follow these steps:

1. Create a class derived from <xref:Metalama.Framework.Serialization.ValueTypeSerializer`1> or <xref:Metalama.Framework.Serialization.ReferenceTypeSerializer`1> class. The class must have a default public constructor.
2. Register the serializer by using the assembly-level <xref:Metalama.Framework.Serialization.ImportSerializerAttribute>.

For generic types, the serializer type must have the same type arguments as the serialized type.

## Security and obfuscation

Although it is inspired by Microsoft's `BinaryFormatter`, which has been deprecated for security reasons, using the <xref:Metalama.Framework.Serialization> namespace does _not_ present any security risk. Although the serializer might, in theory, allow for arbitrary code execution, it is only designed to deserialize binary data stored in a binary library. Since this library also, in essence, allows for arbitrary code execution, the use of the serializer does not increase the risk. Developers should not use untrusted libraries in the first place.

> [!WARNING]
> The <xref:Metalama.Framework.Serialization> namespace is _NOT_ compatible with obfuscation. The serialized binary stream contains full names of declarations in clear text, partially defeating the purpose of serialization. Additionally, serialization will fail if these names are changed after compilation by the obfuscation process.


## Accessing a field after it has been overridden

When you override a field, Metalama turns it into a property. That is, _before_ the aspect, the field will be represented by an object of type <xref:Metalama.Framework.Code.IField> type and exposed in the <xref:Metalama.Framework.Code.INamedType.Fields?text=INamedType.Fields> collection. However, _after_ the aspect, the overridden field is represented as an <xref:Metalama.Framework.Code.IProperty> and exposed in the <xref:Metalama.Framework.Code.INamedType.Properties?text=INamedType.Properties> collection. This usually works well, and most of you likely haven't had to think much about it.

However, the devil is in the details. Things get more complex when you are passing a reference to an overridden field to another aspect or to another assembly using transitive aspects.

If you take a reference to a field _before_ the aspect, you will get an `IRef<IField>`. If you resolve the reference _after_ the aspect, you might wonder what happens because the field is now a property.

If you attempt to resolve an `IRef<IField>`, you will always get an <xref:Metalama.Framework.Code.IField>. If the field has been overridden, you will get an  _shim_ representing what is actually an <xref:Metalama.Framework.Code.IProperty>. However, this field is _not_ navigable through the `INamedType.Fields` properties, but only, as an `IProperty`, through `INamedType.Properties`. You can navigate to the "real" property using the <xref:Metalama.Framework.Code.IField.OverridingProperty?text=IField.OverridingProperty> property. The inverse relationship is the <xref:Metalama.Framework.Code.IProperty.OriginalField?text=IProperty.OriginalField> property. Also, the <xref: Metalama.Framework.Code.IRef.As*?text=IRef.As&lt;&gt;()> method is able to convert an overridden an <xref:Metalama.Framework.Code.IField> into its overriding <xref:Metalama.Framework.Code.IProperty> and conversely.
