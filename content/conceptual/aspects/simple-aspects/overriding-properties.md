---
uid: simple-override-property
level: 200
summary: "The document provides a guide on how to override fields and properties using the Metalama.Framework package in C#, including examples of trimming strings and normalizing values to uppercase."
keywords: "override fields, override properties, Metalama.Framework, OverrideFieldOrPropertyAspect, field transformation, property template, trim strings, normalize uppercase"
created-date: 2023-03-01
modified-date: 2025-11-30
---

# Getting started with overriding fields and properties

In <xref:simple-override-method>, you learned how to change the implementation of a method with an aspect. You can apply the same technique to fields and properties by extending <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect>.

## Overriding a field or property

Follow these steps to create an aspect capable of overriding a field or property:

1. Add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package to your project.

2. Create a new class derived from the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class. The class serves as a custom attribute, so name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect.OverrideProperty> property in plain C#. To call the original implementation, use <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>.

4. Since the aspect is a custom attribute, you can transform a field or property by simply adding the aspect custom attribute to the field or property.

> [!WARNING]
> When applying an aspect to a field, Metalama will automatically transform the field into a property. If the field is used by reference using `ref`, `out`, and `in` keywords, it will result in a compile-time error.

### Example: empty OverrideFieldOrPropertyAspect aspect

The following example demonstrates an empty implementation of <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> applied to a property and a field.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/EmptyOverrideFieldOrProperty.cs name="Empty OverrideFieldOrProperty"]

This aspect performs no specific function, but it transforms the field into a property.

## Getting or setting the underlying property

If you've only worked with methods so far, you're likely familiar with using the `meta.Proceed()` method in your template. This method also works in a property template: when called from the getter, it returns the field or property value; when called from the setter, it sets the field or property to the value of the `value` parameter.

To get the property value from the setter, or to set the property value to something other than the `value` parameter, get or set the <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Target.FieldOrProperty.Value> property.

### Example: trimming strings

This aspect trims whitespace before and after string values before assigning them to the field or property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/Trimmed.cs name="Trimming string fields and properties"]

The aspect doesn't need to modify the getter, so it only calls `meta.Proceed()`, and Metalama replaces this call with the original implementation of the property. You could write `get => meta.Target.FieldOrProperty.Value` instead, achieving the same effect.

The setter is modified to call the `Trim` method on the input `value`. The most concise and simple code is `set => meta.Target.FieldOrProperty.Value = value?.Trim()`. Alternatively, you could write the following code:

```cs
set
{
    value = value?.Trim();
    meta.Proceed();
}
```

### Example: turning the value to uppercase

The following example is similar to the previous one, but instead of trimming a string, it normalizes the value to uppercase.

We apply the aspect to a class representing a shipment between two airports.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/UpperCase.cs name="Changing case to Upper case"]

In this example, `From` is a public field and `To` is a public property. This demonstrates that the aspect works on both because <xref:Metalama.Framework.Code.IFieldOrProperty> is used in the aspect. If you want the aspect to apply only to properties and not to fields, use <xref:Metalama.Framework.Code.IProperty>.

## Going deeper

To go deeper into field/property overrides, explore the following articles:

- This article shows how to use `meta.Proceed` and `meta.Target.Method.Name` in your templates. You can write much more complex and powerful templates, even performing compile-time `if` and `foreach` blocks. To learn how, see <xref:templates>.
- To learn how to override several fields and properties from a single type-level aspect, see <xref:overriding-methods>.

> [!div class="see-also"]
> <xref:templates>
> <xref:overriding-fields-or-properties>
> <xref:simple-override-method>
> <xref:simple-aspects>
> <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect>
> <xref:Metalama.Framework.Advising.GetterTemplateSelector>
> <xref:Metalama.Framework.Code.IFieldOrProperty>
> <xref:Metalama.Framework.Code.IProperty>
