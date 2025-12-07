---
uid: dynamic-typing
level: 300
summary: "Learn how to use dynamic typing in Metalama templates to handle unknown types at compile time, including dynamic expressions, member access, and conversion to IExpression."
keywords: "dynamic typing, templates, meta.This, IExpression, run-time code generation"
created-date: 2025-11-07
modified-date: 2025-11-30
---


# Dynamic typing in templates

When writing a template, you don't generally know in advance the exact type of the declarations to which it's applied.

For example, an aspect may not know the parameter and return types of the methods that it overrides.

There are two mechanisms to represent unknown types: one based on `dynamic` types, and the second based on type parameters. This article focuses on the dynamic typing approach. The generic approach is covered in <xref:template-parameters>.

Metalama uses `dynamic` typing to represent a value of a run-time type. You can also use the `dynamic` keyword in your templates.

For instance, if the parameters and return types of a method are unknown, their types can be `dynamic`.

```cs
dynamic? OverrideMethod()
{
    dynamic p1 = meta.Target.Parameters[0].Value;
    dynamic p2 = meta.Target.Parameters[1].Value;

    Console.WriteLine( $"p1={p1}, p2={p2}." );

    return default;
}
```

All `dynamic` compile-time code is transformed into strongly-typed run-time code. In other words, we use `dynamic` when the expression type is unknown to the template developer, but the type is always known and resolved when the template is applied to a specific target declaration.

> [!WARNING]
> In a template, it isn't possible to generate code that uses `dynamic` typing at _run_ time.

## APIs returning dynamic objects

The `meta` API exposes some properties of the `dynamic` type and some methods returning `dynamic` values. These members are compile-time, but they produce a _C# expression_ that can be used in the run-time code of the template. Because these members return a `dynamic` value, they can be used anywhere in your template. The code won't be validated when the template is compiled but when the template is applied.

For instance, `meta.This` returns a `dynamic` object that represents the expression `this`. Because `meta.This` is `dynamic`, you can write `meta.This._logger` in your template, which translates to `this._logger`. This works even if your template doesn't contain a member named `_logger`. Since `meta.This` returns a `dynamic` type, any field or method accessed through the `meta.This` expression won't be validated when the template is compiled (or in the IDE) but when the template is _expanded_, in the context of a specific target declaration.

The following APIs return `dynamic` values, organized by category:

* Equivalents to the `this` or `base` keywords:
  * <xref:Metalama.Framework.Aspects.meta.This?text=meta.This>, equivalent to the `this` keyword, allows calling arbitrary _instance_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.Base?text=meta.Base>, equivalent to the `base` keyword, allows calling arbitrary _instance_ members of the _base_ of the target type.
  * <xref:Metalama.Framework.Aspects.meta.ThisType?text=meta.ThisType> allows calling arbitrary _static_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.BaseType?text=meta.BaseType> allows calling arbitrary _static_ members of the _base_ of the target type.
* <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> allows getting or setting the value of a compile-time expression in run-time code. It's implemented, for instance, by:
  * `meta.Target.Field.Value`, `meta.Target.Property.Value`, or `meta.Target.FieldOrProperty.Value` allow getting or setting the value of the target field or property.
  * `meta.Target.Parameter.Value` allows getting or setting the value of the target parameter.
  * `meta.Target.Method.Parameters[*].Value` allows getting or setting the value of a target method's parameter.
* _Invokers_, i.e., APIs that, given a compile-time <xref:Metalama.Framework.Code.IMethod>, <xref:Metalama.Framework.Code.IField>, <xref:Metalama.Framework.Code.IProperty>, ... return a `dynamic` object that generates a call to this object. For instance:
  * `method.Invoke( a, b, c )`, or
  * `field.Value`

  For details regarding invokers, see <xref:invokers>.

## Using dynamic expressions

You can write any code to the right of a dynamic expression. As with any dynamically typed code, the syntax of the code is validated, but not the existence of the invoked members.

```cs
// Translates into: this.OnPropertyChanged( "X" );
meta.This.OnPropertyChanged( "X" );
```

You can combine dynamic code and compile-time expressions. In the following snippet, `OnPropertyChanged` is dynamically resolved but `meta.Property.Name` evaluates into a `string`:

```cs
// Translated into: this.OnPropertyChanged( "MyProperty" );
meta.This.OnPropertyChanged( meta.Property.Name );
```

Dynamic expressions can be embedded within larger expressions. In the following example, the dynamic expression is part of a string concatenation:

```cs
// Translates into: Console.WriteLine( "p = " + p );
Console.WriteLine( "p = " + meta.Target.Parameters["p"].Value );
```

### Example: meta.This

In the following aspect, the logging aspect uses `meta.This`, which returns a `dynamic` object, to access the target type. The aspect assumes that the target type defines a field named `_logger` and that the type of this field has a method named `WriteLine`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicTrivial.cs name="meta.This"]

### Example: IFieldOrProperty.Value

In the following example, an aspect looks for any field of type `TextWriter` in the target type. Since the field's name is determined at compile-time (when analyzing the code model) but needs to be used in the generated run-time code, the aspect uses the <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property to generate an expression that accesses the field. This property returns a `dynamic` object, but we cast it to `TextWriter` to enable IntelliSense and compile-time type checking within the template code itself.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicCodeModel.cs name="Invokers"]

### Limitations

> [!WARNING]
> Due to limitations of the C# language, you can't use extension methods on the right side of a dynamic expression.

In this case, you have two options:

1. Call the extension method in the traditional way by specifying its type name on the left and passing the dynamic expression as an argument:

  ```cs
  // Instead of: meta.This.MyCollection.MyExtensionMethod()
  MyExtensions.MyExtensionMethod(meta.This.MyCollection);
  ```

2. Cast the dynamic expression to a specific type if it's known:

  ```cs
  // If you know the type of MyCollection
  ((IEnumerable<string>)meta.This.MyCollection).MyExtensionMethod();
  ```

## Writing to dynamic members

When the expression is writable, the `dynamic` member can be used on the left-hand side of an assignment:

```cs
// Translates into: this.MyProperty = 5;
meta.Property.Value = 5;
```

You can also pass some expressions as a `ref`:

```cs
// Translates into: Interlocked.Increment( ref this.MyField );
Interlocked.Increment( ref meta.Field.Value );
```

### Dynamic local variables

When the template is expanded, `dynamic` local variables are transformed into strongly-typed variables based on the actual type at expansion time, typically represented as `var` in the generated code. Therefore, all `dynamic` variables must be initialized.

## Converting between a dynamic expression and a compile-time IExpression

Under the hood, all `dynamic` values in templates are compile-time objects implementing the <xref:Metalama.Framework.Code.IExpression> interface.

* **Converting dynamic to IExpression.** Whenever you have a `dynamic` expression and need a compile-time <xref:Metalama.Framework.Code.IExpression> object, you can simply cast the `dynamic` into `IExpression`.

* **Converting IExpression to dynamic.** Conversely, when you have an `IExpression` and want a run-time object, use the `IExpression.Value` property to access it as a `dynamic` value.

Instead of using techniques like parsing to generate <xref:Metalama.Framework.Code.IExpression> objects, it can be convenient to write the expression in T#/C# and convert it. This lets you create expressions that depend on compile-time conditions and control flows.

For instance, suppose you want an `IExpression` that represents the `this` parameter for instance methods, or the first parameter for static methods. You can use the following code:

  ```cs
  var thisParameter = meta.Target.Method.IsStatic
                          ? meta.Target.Method.Parameters.First()
                          : (IExpression) meta.This;

  ```

You can now use `thisParameter` in an API that accepts an `IExpression`, for instance:

```csharp
myMethod.Invoke( thisParameter );
```

You can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithType*> and <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithNullability*> extension methods to modify the return type of the returned <xref:Metalama.Framework.Code.IExpression>.

> [!div class="see-also"]
> <xref:invokers>
> <xref:template-overview>
> <xref:template-parameters>
> <xref:run-time-expressions>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.meta>
> <xref:Metalama.Framework.Code.IExpression>
