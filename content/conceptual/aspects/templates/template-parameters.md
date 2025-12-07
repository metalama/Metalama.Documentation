---
uid: template-parameters
level: 300
summary: "This document describes how to use compile-time parameters and type parameters in the BuildAspect method in Metalama and provides examples and alternatives for using these parameters."
keywords: "compile-time parameters, type parameters, BuildAspect method, Metalama, template method, compile-time-only, tags"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Template parameters and type parameters

Compile-time parameters enable your `BuildAspect` implementation to pass arguments to the template. There are two types of template parameters: regular parameters and type parameters (also referred to as generic parameters).

In contrast to run-time parameters:

* Compile-time parameters must receive a value at compile time from the `BuildAspect` method.
* Compile-time parameters aren't visible in the generated code, meaning they're removed from the parameter list when the template is expanded.

## Regular parameters

Compile-time parameters are particularly advantageous when the same template is used multiple times by the aspect. For example, when introducing a method for each field of a type, the method needs to know which field it should handle.

To define and use a compile-time parameter in a template method:

1. Add one or more parameters to the template method and annotate them with the <xref:Metalama.Framework.Aspects.CompileTimeAttribute?text=[CompileTime]> custom attribute. The parameter type must not be run-time-only. If the parameter type is compile-time-only (for instance, `IField`), the custom attribute is superfluous.

2. In your `BuildAspect` method implementation, when calling an advice method, pass the parameter values as an anonymous object to the `args` argument. For instance, `args: new { a = "", b = 3, c = field }` where `a`, `b`, and `c` are the exact names of the template parameters (the name matching is case-sensitive).

### Example: regular parameters

```csharp
// Template method with compile-time parameter
[Template]
private dynamic? LogMethod([CompileTime] string methodName)
{
    Console.WriteLine($"Entering {methodName}");
    return meta.Proceed();
}

// BuildAspect passes the parameter value
public override void BuildAspect(IAspectBuilder<IMethod> builder)
{
    builder.Override(
        nameof(LogMethod),
        args: new { methodName = builder.Target.Name } );
}
```

### Alternative: tags

If you can't use compile-time parameters (typically because you have a field, property, or event template instead of a method template), substitute them with tags. For details about tags, refer to <xref:sharing-state-with-advice>. The advantage of compile-time parameters over tags is that template parameters enhance code readability, while tags require a more complex syntax.

## Type parameters

Compile-time type parameters, also known as compile-time generic parameters, are generic parameters whose value is defined at compile time by the `BuildAspect` method. Compile-time type parameters offer a type-safe alternative to dynamic typing in templates. With compile-time type parameters, referencing a type from a template is more convenient, as it can be referred to as a type rather than using a more complex syntax such as `meta.Cast`.

To define and use a compile-time type parameter in a template method, follow the same steps as for a standard compile-time parameter:

1. Add one or more type parameters to the template method and annotate them with the <xref:Metalama.Framework.Aspects.CompileTimeAttribute?text=[CompileTime]> custom attribute. The type parameter can have arbitrary constraints. The current version of Metalama will ignore them when expanding the template.

2. In your `BuildAspect` method implementation, when calling an advice method, pass the parameter values as an anonymous object to the `args` argument. For instance, `args: new { T1 = typeof(int), T2 = field.Type }` where `T1` and `T2` are the exact names of the template parameters (note that the name matching is case-sensitive).

## Example: type parameters

```csharp
// Template method with compile-time type parameter
[Template]
private T GetDefault<[CompileTime] T>()
{
    return default(T);
}

// BuildAspect passes the type parameter value
public override void BuildAspect(IAspectBuilder<IProperty> builder)
{
    builder.WithDeclaringType().IntroduceMethod(
        nameof(GetDefault),
        args: new { T = builder.Target.Type }, // Assign the property type.
        buildMethod: m => m.Name = $"GetDefaultFor{builder.Target.Name}" );
}
```

### Alternative: dynamic typing

A viable alternative to compile-time type parameters is dynamic typing and using methods like `meta.Cast` or abstractions like <xref:Metalama.Framework.Code.IExpression>. For details about generating run-time code, refer to <xref:run-time-expressions>.

## Example

The following aspect generates, for each field or property `Bar`, a method named `ResetBar` that sets the field or property to its default value.

The `Reset` template method accepts two compile-time parameters:

* A standard parameter `field` that contains the field or property to which the template pertains.
* A type parameter `T` that contains the type of the field or property. This type parameter is used to generate the `default(T)` syntax, where `T` is replaced by the actual field or property when the template is expanded.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GenerateResetMethods.cs name="Generate Reset Methods"]

> [!div class="see-also"]
> <xref:templates>
> <xref:template-overview>
> <xref:dynamic-typing>
> <xref:sharing-state-with-advice>
> <xref:Metalama.Framework.Aspects.CompileTimeAttribute>
> <xref:Metalama.Framework.Aspects.meta.Tags>
