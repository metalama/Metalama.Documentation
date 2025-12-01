---
uid: wpf-dependency-property
level: 100
keywords: "dependency property, WPF, Metalama, boilerplate code, automatic property, DependencyPropertyAttribute, validation, PropertyChanged callback, default values, .NET"
created-date: 2024-11-06
modified-date: 2025-11-30
---

# WPF Dependency Property

A dependency property in WPF is a kind of property that can be set from the WPF markup language and bound to a property of another object (such as a View-Model object) using a <xref:System.Windows.Data.Binding> element. Unlike C# properties, dependency properties must be programmatically registered using `DependencyProperty.Register`. To expose a dependency property as a C# property, one typically writes boilerplate code as demonstrated in the following example:

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

Instead of writing this boilerplate, you can simply add the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect to a C# automatic property to convert it into a WPF dependency property:

```cs
class MyClass
{
    [DependencyProperty]
    public bool IsEnabled { get; set; }
}
```

The <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect implements the following features and benefits:

* Zero boilerplate.
* Integration with `Metalama.Patterns.Contracts` to validate dependency properties using aspects like <xref:Metalama.Patterns.Contracts.NotNullAttribute?text=[NotNull]> or <xref:Metalama.Patterns.Contracts.UrlAttribute?text=[Url]>. See <xref:contract-patterns> for details.
* Support for custom pre- and post-assignment callbacks.
* Detection of mutable or read-only dependency properties based on property accessor accessibility.
* Handling of default values.

## Creating a dependency property

To create a dependency property using the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect, follow these steps:

1. Add a reference to the `Metalama.Patterns.Wpf` package to your project.
2. Open a class derived from <xref:System.Windows.DependencyObject>, such as a window or a user control.
3. Add an automatic property to this class.
4. Add the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> custom attribute to this automatic property.
5. Optionally, add any contract from the `Metalama.Patterns.Contracts` package to the automatic property. See <xref:contract-patterns> for details about contracts.

### Example: a simple dependency property

The following example demonstrates the code generation pattern for a standard property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Simple.cs]

### Example: a read-only dependency property

In the following example, the automatic property has a private setter. The <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect generates a read-only dependency property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/ReadOnly.cs]

## Adding validation through a contract

The most straightforward way to add validation to a dependency property is to add an aspect from the `Metalama.Patterns.Contracts` package.

### Example: a dependency property with contracts

In the following example, a `[Positive]` contract is added to the automatic property. You can see how the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyProperty]> aspect generates code to enforce this precondition.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Contract.cs]


## Adding validation through a callback method

The second way to add validation to a dependency property is by adding a callback method to your code. For a property named `Foo`, the validation method must be named `ValidateFoo` and have one of the following signatures:

* `static void ValidateFoo(TPropertyType value)`
* `static void ValidateFoo(DependencyProperty property, TPropertyType value)`
* `static void ValidateFoo(TDeclaringType instance, TPropertyType value)`
* `static void ValidateFoo(DependencyProperty property, TDeclaringType instance, TPropertyType value)`
* `void ValidateFoo(TPropertyType value)`
* `void ValidateFoo(DependencyProperty property, TPropertyType value)`

where `TDeclaringType` is the declaring type of the target property, `DependencyObject`, or `object`, and where `TPropertyType` is any type assignable from the actual type of the target property. `TPropertyType` can also be a generic type parameter, in which case the method must have exactly one generic parameter.

If you prefer specifying the validation method explicitly instead of relying on a naming convention, you can do it using the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.ValidateMethod?text=DependencyPropertyAttribute.ValidateMethod> property.

These methods must throw an exception in case of an invalid value.

### Example: validation callback

The following example implements a profanity filter on a dependency property. If the value contains the word `foo`, it will throw an exception.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/Validate.cs]

## Handling of default values

When an automatic property is initialized with a value, not from the constructor but from the property declaration itself, this expression is used as the _default_ value of the dependency property. The concept of _default value_ of a property in WPF means that if you attempt to set a property to its default value in a XAML file, the assignment will be grayed out as redundant.

> [!NOTE]
> When an automatic property is initialized to a value, this value is also assigned to the property from the instance constructor of the object to mimic the behavior of a C# automatic property. Note that there is a slight difference: in standard automatic properties, the initial value is assigned _before_ the base constructor is executed. However, with a dependency property, the value is assigned _after_ the base constructor is invoked.

The following example demonstrates the code generation pattern when an automatic property is initialized to a value.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/DefaultValue.cs]

If you do not want the property initial value to be interpreted as the default value of the dependency property, you can disable this behavior by setting the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.InitializerProvidesDefaultValue> property to `false`. This property is available both as an attribute property on the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute> class and through the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyExtensions.ConfigureDependencyProperty*> fabric extension method.


## Adding a PropertyChanged callback

Whereas the validate method executes _before_ the assignment, you can also add code that executes _after_ the assignment of a dependency property to its new value. For a property named `Foo`, add a method named `OnFooChanged` of one of these signatures:

* `static void OnFooChanged()`
* `static void OnFooChanged(DependencyProperty property)`
* `static void OnFooChanged(TDeclaringType instance)`
* `static void OnFooChanged(DependencyProperty property, TDeclaringType instance)`
* `void OnFooChanged()`
* `void OnFooChanged(DependencyProperty property)`
* `void OnFooChanged(TPropertyType value)`
* `void OnFooChanged(DependencyProperty oldValue, DependencyProperty newValue)`
* `void OnFooChanged<T>(T value)`
* `void OnFooChanged<T>(T oldValue, T newValue)`

As with the validate method, you can explicitly identify the property-changed method instead of relying on a naming convention thanks to the <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute.PropertyChangedMethod?text=DependencyPropertyAttribute.PropertyChangedMethod> property.

### Example: post-assignment callback

In the following example, the `OnBorderWidthChanged` method is executed after the value of the `BorderWidth` property has changed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/OnPropertyChanged.cs]

## Customizing naming conventions

All examples above relied on the default naming convention, which is based on the following assumptions:

* Given a property named `Foo`:
    * The name of the field containing the <xref:System.Windows.DependencyProperty> object is `FooProperty`.
    * The name of the validation method is `ValidateFoo`.
    * The name of the post-assignment callback is `OnFooChanged`.

This naming convention can be modified by calling the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyExtensions.ConfigureDependencyProperty*> fabric extension method, then <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyOptionsBuilder.AddNamingConvention*?text=builder.AddNamingConvention>, and supplying an instance of the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention> class.

If specified, the <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.PropertyNamePattern?text=DependencyPropertyNamingConvention.PropertyNamePattern> is a regular expression that matches the name of the WPF dependency property from the name of the C# property. If this property is unspecified, the default matching algorithm is used, i.e., the name of the dependency property equals the name of the C# property. The <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.OnPropertyChangedPattern> and <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.ValidatePattern> properties are regular expressions that match the validate and property-changed methods. The <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.RegistrationFieldName> property represents the name of the field containing the <xref:System.Windows.DependencyProperty> object. In these expressions, the `{PropertyName}` substring is replaced by the name of the dependency property returned by <xref:Metalama.Patterns.Wpf.Configuration.DependencyPropertyNamingConvention.PropertyNamePattern>.

Naming conventions are evaluated by priority order. The default priority is the one in which the convention has been added. It can be overwritten by supplying a value to the `priority` parameter.

The default naming convention is evaluated last and cannot be modified.

### Example: Czech naming convention

Here is an illustration of a coding convention for the Czech language.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Wpf/DependencyProperties/NamingConvention.cs]

> [!div class="see-also"]
> <xref:wpf>
> <xref:wpf-command>
> <xref:contract-patterns>

