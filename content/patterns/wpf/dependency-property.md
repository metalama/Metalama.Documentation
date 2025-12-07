---
uid: wpf-dependency-property
summary: Convert automatic properties into WPF dependency properties with the [DependencyProperty] aspect.
keywords: dependency property, WPF, Metalama, boilerplate code, automatic property, DependencyPropertyAttribute, validation, PropertyChanged callback, default values, .NET
level: 100
created-date: 2024-11-06
modified-date: 2025-11-30
---

# WPF dependency properties

A dependency property in WPF is a property you can set from the WPF markup language and bind to a property of another object (like a view model) using a <xref:System.Windows.Data.Binding> element. Unlike C# properties, you must programmatically register dependency properties using `DependencyProperty.Register`. To expose a dependency property as a C# property, you typically write boilerplate code as demonstrated in the following example:

```cs
class MyClass
{
    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register( nameof(IsEnabled), typeof(bool), typeof(MyClass));

    public bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }
}
```

Instead of writing this boilerplate, add the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect to a C# automatic property to convert it into a WPF dependency property:

```cs
class MyClass
{
    [DependencyProperty]
    public bool IsEnabled { get; set; }
}
```

The <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect provides:

- Zero boilerplate.
- Integration with [Metalama.Patterns.Contracts](https://www.nuget.org/packages/Metalama.Patterns.Contracts) to validate dependency properties using aspects like <xref:Metalama.Patterns.Contracts.NotNullAttribute?text=[NotNull]> or <xref:Metalama.Patterns.Contracts.UrlAttribute?text=[Url]> (for details, see <xref:contract-patterns>).
- Support for custom pre- and post-assignment callbacks.
- Detection of mutable or read-only dependency properties based on property accessor accessibility.
- Handling of default values.

## Creating a dependency property

To create a dependency property using the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect:

1. Add the [Metalama.Patterns.Wpf](https://www.nuget.org/packages/Metalama.Patterns.Wpf) package to your project.
2. Open a class derived from <xref:System.Windows.DependencyObject>, such as a window or user control.
3. Add an automatic property to this class.
4. Add the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> custom attribute to this automatic property.
5. Optionally, add any contract from the [Metalama.Patterns.Contracts](https://www.nuget.org/packages/Metalama.Patterns.Contracts) package to the automatic property. For details about contracts, see <xref:contract-patterns>.

### Example: a simple dependency property

The following example demonstrates the code generation pattern for a standard property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Simple.cs]

### Example: a read-only dependency property

In the following example, the automatic property has a private setter. The <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect generates a read-only dependency property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/ReadOnly.cs]

## Adding validation through a contract

The most straightforward way to add validation to a dependency property is to add an aspect from the [Metalama.Patterns.Contracts](https://www.nuget.org/packages/Metalama.Patterns.Contracts) package.

### Example: a dependency property with contracts

In the following example, a `[Positive]` contract is added to the automatic property. The <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect generates code to enforce this precondition.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Contract.cs]

## Adding validation through a callback method

You can also add validation to a dependency property by adding a callback method to your code. For a property named `Foo`, name the validation method `ValidateFoo` with one of the following signatures:

- `static void ValidateFoo(TPropertyType value)`
- `static void ValidateFoo(DependencyProperty property, TPropertyType value)`
- `static void ValidateFoo(TDeclaringType instance, TPropertyType value)`
- `static void ValidateFoo(DependencyProperty property, TDeclaringType instance, TPropertyType value)`
- `void ValidateFoo(TPropertyType value)`
- `void ValidateFoo(DependencyProperty property, TPropertyType value)`

where `TDeclaringType` is the declaring type of the target property, `DependencyObject`, or `object`, and `TPropertyType` is any type assignable from the actual type of the target property. `TPropertyType` can also be a generic type parameter, in which case the method must have exactly one generic parameter.

To specify the validation method explicitly instead of relying on a naming convention, use the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.ValidateMethod?text=DependencyPropertyAttribute.ValidateMethod> property.

These methods must throw an exception when the value is invalid.

### Example: validation callback

The following example implements a profanity filter on a dependency property. If the value contains the word `foo`, it throws an exception.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Validate.cs]

## Handling default values

When you initialize an automatic property with a value in the property declaration (not from the constructor), this expression is used as the _default value_ of the dependency property. In WPF, if you attempt to set a property to its default value in a XAML file, the assignment is grayed out as redundant.

> [!NOTE]
> When you initialize an automatic property to a value, this value is also assigned to the property from the instance constructor to mimic the behavior of a C# automatic property. There's a slight difference: in standard automatic properties, the initial value is assigned _before_ the base constructor executes. However, with a dependency property, the value is assigned _after_ the base constructor is invoked.

The following example demonstrates the code generation pattern when an automatic property is initialized to a value.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/DefaultValue.cs]

If the property initial value shouldn't be interpreted as the default value of the dependency property, disable this behavior by setting <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.InitializerProvidesDefaultValue> to `false`. This property is available both as an attribute property on the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute> class and through the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyExtensions.ConfigureDependencyProperty*> fabric extension method.

## Adding a PropertyChanged callback

While the validate method executes _before_ the assignment, you can also execute code _after_ assigning a dependency property to its new value. For a property named `Foo`, add a method named `OnFooChanged` with one of these signatures:

- `static void OnFooChanged()`
- `static void OnFooChanged(DependencyProperty property)`
- `static void OnFooChanged(TDeclaringType instance)`
- `static void OnFooChanged(DependencyProperty property, TDeclaringType instance)`
- `void OnFooChanged()`
- `void OnFooChanged(DependencyProperty property)`
- `void OnFooChanged(TPropertyType value)`
- `void OnFooChanged(DependencyProperty oldValue, DependencyProperty newValue)`
- `void OnFooChanged<T>(T value)`
- `void OnFooChanged<T>(T oldValue, T newValue)`

As with the validate method, explicitly identify the property-changed method instead of relying on a naming convention by using the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.PropertyChangedMethod?text=DependencyPropertyAttribute.PropertyChangedMethod> property.

### Example: post-assignment callback

In the following example, the `OnBorderWidthChanged` method is executed after the value of the `BorderWidth` property has changed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/OnPropertyChanged.cs]

## Customizing naming conventions

The preceding examples rely on the default naming convention, which is based on the following assumptions:

- Given a property named `Foo`:
  - The name of the field containing the <xref:System.Windows.DependencyProperty> object is `FooProperty`.
  - The name of the validation method is `ValidateFoo`.
  - The name of the post-assignment callback is `OnFooChanged`.

Modify this naming convention by calling the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyExtensions.ConfigureDependencyProperty*> fabric extension method, then <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyOptionsBuilder.AddNamingConvention*?text=builder.AddNamingConvention>, and supplying an instance of the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention> class.

If specified, the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.PropertyNamePattern?text=DependencyPropertyNamingConvention.PropertyNamePattern> is a regular expression that matches the name of the WPF dependency property from the name of the C# property. If unspecified, the default matching algorithm is used (the dependency property name equals the C# property name). The <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.OnPropertyChangedPattern> and <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.ValidatePattern> properties are regular expressions that match the validate and property-changed methods. The <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.RegistrationFieldName> property represents the name of the field containing the <xref:System.Windows.DependencyProperty> object. In these expressions, the `{PropertyName}` substring is replaced by the dependency property name returned by <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.PropertyNamePattern>.

Naming conventions are evaluated by priority order. The default priority is the order in which you added the convention. Override it by supplying a value to the `priority` parameter.

The default naming convention is evaluated last and can't be modified.

### Example: Czech naming convention

Here is an illustration of a coding convention for the Czech language.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/NamingConvention.cs]

> [!div class="see-also"]
> <xref:wpf>
> <xref:wpf-command>
> <xref:contract-patterns>
