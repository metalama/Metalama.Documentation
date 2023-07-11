---
uid: template-parameters
level: 200
---

# Template parameters and type parameters

Compile-time parameters allow your `BuildAspect` implementation to pass arguments to the template. There are two types of template parameters: standard and type parameters (also known as generic parameters).

Unlike run-time parameters:

* Compile-time parameters must receive a value at compile time from the `BuildAspect` method.
* Compile-time parameters are not visible in the generated code, meaning they are removed from the parameter list when the template is expanded.

## Parameters

Compile-time parameters are particularly useful when the same template is used multiple times by the aspect. For instance, when you introduce a method for each field of a type, and the method needs to know which field it must handle.

To define and use a compile-time parameter in a template method:

1. Add one or more parameters to the template method and annotate them with the <xref:Metalama.Framework.Aspects.CompileTimeAttribute> custom attribute. The parameter type must not be run-time-only. If the parameter type is compile-time-only (for example, `IField`), the custom attribute is redundant.

2. In your `BuildAspect` method implementation, when adding the advice by calling a method of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface, pass the parameter values as an anonymous object to the `args` argument. For example, `args: new { a = "", b = 3, c = field }` where `a`, `b`, and `c` are the exact names of the template parameters (the name matching is case-sensitive).

### Alternative: tags

If you cannot use compile-time parameters (typically because you have a field, property, or event template instead of a method template), you can replace them with tags. For details about tags, see <xref:sharing-state-with-advice>. The advantage of compile-time parameters over tags is that template parameters make the code more readable, whereas tags require a more complex syntax.

## Type parameters

Compile-time type parameters, also known as compile-time generic parameters, are generic parameters whose value is specified at compile time by the `BuildAspect` method. Compile-time type parameters are a type-safe alternative to dynamic typing in templates. With compile-time type parameters, it is more convenient to reference a type from a template since it can be referenced as a type, instead of using a more complex syntax like `meta.Cast`.

To define and use a compile-time type parameter in a template method, follow the same steps as for a standard compile-time parameter:

1. Add one or more type parameters to the template method and annotate them with the <xref:Metalama.Framework.Aspects.CompileTimeAttribute> custom attribute. The type parameter can have arbitrary constraints. The current version of Metalama will ignore them when expanding the template.

2. In your `BuildAspect` method implementation, when adding the advice by calling a method of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface, pass the parameter values as an anonymous object to the `args` argument. For example, `args: new { T1 = typeof(int), T2 = field.Type }` where `T1` and `T2` are the exact names of the template parameters (note that the name matching is case-sensitive).

### Alternative: dynamic typing

An alternative to compile-time type parameters is dynamic typing and using methods like `meta.Cast` or abstractions like <xref:Metalama.Framework.Code.IExpression>. For details about generating run-time code, see <xref:template-dynamic-code>.

## Example

The following aspect generates, for each field or property `Bar`, a method named `ResetBar`, which sets the field or property to its default value.

The `Reset` template method accepts two compile-time parameters:

* A standard parameter `field` that contains the field or property to which the template relates.
* A type parameter `T` that contains the type of the field or property. This type parameter is used to generate the `default(T)` syntax, where `T` is replaced by the actual field or property when the template is expanded.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/GenerateResetMethods.cs name="Generate Reset Methods"]
