---
uid: overriding-fields-or-properties
level: 300
summary: "This document provides advanced usage scenarios for the Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect class, including accessing metadata, resolving dependencies, overriding multiple fields or properties, and using property and accessor templates."
keywords: "Overriding fields, OverrideFieldOrPropertyAspect, resolving dependencies, service locator pattern, property template, accessor template, dynamic templates"
created-date: 2023-02-16
modified-date: 2024-08-04
---

# Overriding fields or properties

In <xref:simple-override-property>, you learned the basics of the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> class. In this section, we will cover more advanced scenarios.

## Accessing the metadata of the overridden field or property

The metadata of the overridden field or property can be accessed from the template accessors on the <xref:Metalama.Framework.Aspects.IMetaTarget.FieldOrProperty?text=meta.Target.FieldOrProperty> property. This property provides all information about the field or property's name, type, and custom attributes. For instance, the member name is available on `meta.Target.FieldOrProperty.Name` and its type on `meta.Target.FieldOrProperty.Type`.

- `meta.Target.FieldOrProperty` exposes the current field or property as an <xref:Metalama.Framework.Code.IFieldOrProperty>, which reveals characteristics common to fields and properties.
- `meta.Target.Field` exposes the current field as an <xref:Metalama.Framework.Code.IField>, but will throw an exception if the target is not a field.
- `meta.Target.Property` exposes the current field as an <xref:Metalama.Framework.Code.IProperty>, but will throw an exception if the target is not a property.
- `meta.Target.Method` exposes the current accessor method. This works even if the target is a field because Metalama creates pseudo methods to represent field accessors.

To access the _value_ of the field or property, you can use the `meta.Target.FieldOrProperty.Value` expression both in reading and writing. In the setter template, `meta.Target.Parameters[0].Value` gives you the value of the `value` parameter.

### Example: Resolving dependencies on the fly

The following example is a simplified implementation of the service locator pattern.

The `Import` aspect overrides the getter of a property to make a call to a global service locator. The type of the service is determined from the type of the field or property, using `meta.Target.FieldOrProperty.Type`. The dependency is not stored, so the service locator must be called every time the property is evaluated.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImport.cs name="Import Service"]

### Example: Resolving dependencies on the fly and storing the result

This example builds on the previous one, but the dependency is stored in the field or property after it has been retrieved from the service provider for the first time.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImportWithSetter.cs name="Import Service"]

## Overriding several fields or properties from the same aspect

Similar to methods, to override one or more fields or properties from a single aspect, your aspect needs to implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method exposed on `builder.Advice`. Your implementation must then call the <xref:Metalama.Framework.Advising.AdviserExtensions.Override*?text=builder.Advice.Override> method.

Alternatively, you can call the <xref:Metalama.Framework.Advising.AdviserExtensions.OverrideAccessors*?text=builder.Advice.OverrideAccessors> method, which accepts one or two _accessor_ templates, i.e., one template _method_ for the getter and/or one other method for the setter.

### Using a property template

The _first argument_ of `Override` is the <xref:Metalama.Framework.Code.IFieldOrProperty> that you want to override. This field or property must be in the type targeted by the current aspect instance.

The _second argument_ of `Override` is the name of the template property. This property must exist in the aspect class and, additionally:

* the template property must be annotated with the `[Template]` attribute,
* the template property must be of type `dynamic` (_dynamically-typed_ template), or a type compatible with the type of the overridden property (_strongly-typed_ template).
* the template property can have a setter, a getter, or both. If one accessor is not specified in the template, the corresponding accessor in the target code will not be overridden.

#### Example: registry-backed class

The following aspect overrides properties so that they are written to and read from the Windows registry.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/RegistryStorage.cs name="Registry Storage"]

#### Example: string normalization

This example illustrates a strongly-typed property template with a single accessor that uses the `meta.Target.FieldOrProperty.Value` expression to access the underlying field or property.

The following aspect can be applied to fields or properties of type `string`. It overrides the setter to trim and lowercase the assigned value.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Normalize.cs name="Normalize"]

### Using an accessor template

Advising fields or properties with the `Override` method has the following limitations over the use of `OverrideAccessors`:

* You cannot choose a template for each accessor separately.
* You cannot have generic templates.

To alleviate these limitations, you can use the method <xref:Metalama.Framework.Advising.AdviserExtensions.OverrideAccessors*> and provide one or two method templates: a getter template and/or a setter template.

The templates must fulfill the following conditions:

* Both templates must be annotated with the `[Template]` attribute.
* The getter template must be of signature `T Getter()`, where `T` is either `dynamic` or a type compatible with the target field or property.
* The setter template must be of signature `void Setter(T value)`, where the name `value` of the first parameter is mandatory.

[comment]: # (TODO: example)

