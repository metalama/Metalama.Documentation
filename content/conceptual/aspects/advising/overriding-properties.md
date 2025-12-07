---
uid: overriding-fields-or-properties
level: 300
summary: "This document provides advanced usage scenarios for the Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect class, including accessing metadata, resolving dependencies, overriding multiple fields or properties, and using property and accessor templates."
keywords: "Overriding fields, OverrideFieldOrPropertyAspect, resolving dependencies, service locator pattern, property template, accessor template, dynamic templates"
created-date: 2023-02-16
modified-date: 2025-11-30
---

# Overriding fields or properties

In <xref:simple-override-property>, you learned the basics of the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> class. This article covers more advanced scenarios.

## Accessing metadata of the overridden field or property

The metadata of the overridden field or property can be accessed from the template accessors on the <xref:Metalama.Framework.Aspects.IMetaTarget.FieldOrProperty?text=meta.Target.FieldOrProperty> property. This property provides all information about the field or property's name, type, and custom attributes. For instance, the member name is available on <xref:Metalama.Framework.Code.INamedDeclaration.Name?text=meta.Target.FieldOrProperty.Name> and its type on <xref:Metalama.Framework.Code.IHasType.Type?text=meta.Target.FieldOrProperty.Type>.

- <xref:Metalama.Framework.Aspects.IMetaTarget.FieldOrProperty?text=meta.Target.FieldOrProperty> exposes the current field or property as an <xref:Metalama.Framework.Code.IFieldOrProperty>, which reveals characteristics common to fields and properties.
- <xref:Metalama.Framework.Aspects.IMetaTarget.Field?text=meta.Target.Field> exposes the current field as an <xref:Metalama.Framework.Code.IField>, but throws an exception if the target is not a field.
- <xref:Metalama.Framework.Aspects.IMetaTarget.Property?text=meta.Target.Property> exposes the current property as an <xref:Metalama.Framework.Code.IProperty>, but throws an exception if the target is not a property.
- <xref:Metalama.Framework.Aspects.IMetaTarget.Method?text=meta.Target.Method> exposes the current accessor method. This works even if the target is a field because Metalama creates pseudo methods to represent field accessors.

To access the _value_ of the field or property, use the <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Target.FieldOrProperty.Value> expression for both reading and writing. In the setter template, <xref:Metalama.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters[0].Value> gives you the value of the `value` parameter.

### Example: Resolving dependencies on the fly

The following example is a simplified implementation of the service locator pattern.

The `Import` aspect overrides the getter of a property to call a global service locator. The aspect determines the service type from the field or property type using `meta.Target.FieldOrProperty.Type`. The dependency isn't stored, so the aspect calls the service locator every time the property is accessed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImport.cs name="Import Service"]

### Example: Resolving dependencies on the fly and storing the result

This example builds on the previous one but stores the dependency in the field or property after first retrieval from the service provider.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImportWithSetter.cs name="Import Service"]

## Overriding several fields or properties from the same aspect

To override one or more fields or properties from a single aspect, implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method and call the <xref:Metalama.Framework.Aspects.AdviserExtensions.Override*?text=builder.Advice.Override> method.

Alternatively, call the <xref:Metalama.Framework.Aspects.AdviserExtensions.OverrideAccessors*?text=builder.Advice.OverrideAccessors> method, which accepts one or two accessor templates: one template method for the getter and/or one for the setter.

### Using a property template

The _first argument_ of `Override` is the <xref:Metalama.Framework.Code.IFieldOrProperty> that you want to override. This field or property must be in the type targeted by the current aspect instance.

The _second argument_ of `Override` is the name of the template property. This property must exist in the aspect class and, additionally:

- The template property must be annotated with the `[Template]` attribute.
- The template property must be of type `dynamic` (_dynamically-typed_ template), or a type compatible with the type of the overridden property (_strongly-typed_ template).
- The template property can have a setter, a getter, or both. If one accessor isn't specified in the template, the corresponding accessor in the target code won't be overridden.

#### Example: Registry-backed class

The following aspect overrides properties so that they are written to and read from the Windows registry.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/RegistryStorage.cs name="Registry Storage"]

#### Example: String normalization

This example illustrates a strongly-typed property template with a single accessor that uses the <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Target.FieldOrProperty.Value> expression to access the underlying field or property.

The following aspect can be applied to fields or properties of type `string`. It overrides the setter to trim and convert to lowercase the assigned value.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Normalize.cs name="Normalize"]

### Using an accessor template

The `Override` method has these limitations compared to `OverrideAccessors`:

- You cannot choose a template for each accessor separately.
- You cannot have generic templates.

To overcome these limitations, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.OverrideAccessors*> method and provide one or two method templates: a getter template and/or a setter template.

The templates must fulfill the following conditions:

- Both templates must be annotated with the `[Template]` attribute.
- The getter template must be of signature `T Getter()`, where `T` is either `dynamic` or a type compatible with the target field or property.
- The setter template must be of signature `void Setter(T value)`, where the parameter name `value` is mandatory.

#### Example: Logging property setters

This example demonstrates `OverrideAccessors` with only a setter template. The aspect iterates over properties and overrides those with setters, skipping read-only properties like `FullName`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/LogSetters.cs name="Log Setters"]

> [!div class="see-also"]
> <xref:simple-override-property>
> <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.Override*>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.OverrideAccessors*>
> <xref:Metalama.Framework.Advising.GetterTemplateSelector>
