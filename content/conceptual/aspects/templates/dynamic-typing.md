---
uid: dynamic-typing
level: 300
summary: ""
keywords: ""
created-date: 2025-11-07
modified-date: 2025-11-07
---


# Dynamic typing in templates

When writing a template, you do not generally know in advance the exact type of the declarations to which it is applied.

For example, an aspect may not know the parameter and return types of the methods that it overrides.

There are two mechanisms to represent unknown types: one based no `dynamic` types, the second based on type parameters. Let's now focus on the first one. The generic approach is covered in <xref:template-parameters>.

Metalama uses `dynamic` typing to represent a value of a run-time type. You can also use the `dynamic` keyword in your templates.

For instance, if the parameter and return type of a method are unknown, their type can be `dynamic`.

```cs
dynamic? OverrideMethod()
{
    dynamic p1 = meta.Target.Parameters[0].Value; 
    dynamic p2 = meta.Target.Parameters[1].Value; 

    Console.WriteLine( $"p1={p1}, p2={p2}." );

    return default;
}
```

All `dynamic` compile-time code is transformed into strongly-typed run-time code. That is, we use `dynamic` when the expression type is unknown to the template developer, but the type is always known when the template is applied.

> [!WARNING]
> In a template, it is not possible to generate code that uses `dynamic` typing at _run_ time.

## APIs returning dynamic objects

The `meta` API exposes some properties of the `dynamic` type and some methods returning `dynamic` values. These members are compile-time, but they produce a _C# expression_ that can be used in the run-time code of the template. Because these members return a `dynamic` value, they can be utilized anywhere in your template. The code will not be validated when the template is compiled but when the template is applied.

For instance, `meta.This` returns a `dynamic` object that represents the expression `this`. Because `meta.This` is `dynamic`, you can write `meta.This._logger` in your template, which will translate to `this._logger`. This will work even if your template does not contain a member named `_logger` because `meta.This` returns a `dynamic`; therefore, any field or method referenced on the right hand of the `meta.This` expression will not be validated when the template is compiled (or in the IDE) but when the template is _expanded_, in the context of a specific target declaration.

Here are a few examples of APIs that return a `dynamic`:

* Equivalents to the `this` or `base` keywords:
  * <xref:Metalama.Framework.Aspects.meta.This?text=meta.This>, equivalent to the `this` keyword, allows calling arbitrary _instance_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.Base?text=meta.Base>, equivalent to the `base` keyword, allows calling arbitrary _instance_ members of the _base_ of the target type.
  * <xref:Metalama.Framework.Aspects.meta.ThisType?text=meta.ThisType> allows calling arbitrary _static_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.BaseType?text=meta.BaseType> allows calling arbitrary _static_ members of the _base_ of the target type.
* <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> allows getting or setting the value of a compile-time expression in run-time code. It is implemented, for instance, by:
  * `meta.Target.Field.Value`, `meta.Target.Property.Value`, or `meta.Target.FieldOrProperty.Value` allow getting or setting the value of the target field or property.
  * `meta.Target.Parameter.Value` allows getting or setting the value of the target parameter.
  * `meta.Target.Method.Parameters[*].Value` allows getting or setting the value of a target method's parameter.
* _Invokers_, i.e., APIs that, given a compile-time <xref:Metalama.Framework.Code.IMethod>, <xref:Metalama.Framework.Code.IField>, <xref:Metalama.Framework.Code.IProperty>, ... return a `dynamic` object that generates a call to this object. For instance:
    * `method.Invoke( a, b, c )`, or
    * `field.Value`

  For details regarding invokers, see <xref:invokers>.

## Using dynamic expressions

You can write any dynamic code on the left of a dynamic expression. As with any dynamically typed code, the syntax of the code is validated, but not the existence of the invoked members.

```cs
// Translates into: this.OnPropertyChanged( "X" );
meta.This.OnPropertyChanged( "X" );
```

You can combine dynamic code and compile-time expressions. In the following snippet, `OnPropertyChanged` is dynamically resolved but `meta.Property.Name` evaluates into a `string`:

```cs
// Translated into: this.OnPropertyChanged( "MyProperty" );
meta.This.OnPropertyChanged( meta.Property.Name );
```

Dynamic expressions can appear anywhere in an expression. In the following example, it is part of a string concatenation expression:

```cs
// Translates into: Console.WriteLine( "p = " + p );
Console.WriteLine( "p = " + meta.Target.Parameters["p"].Value );
```

> [!WARNING]
> Due to the limitations of the C# language, you cannot use extension methods on the right part of a dynamic expression. In this case, you must call the extension method in the traditional way, by specifying its type name on the left and passing the dynamic expression as an argument. An alternative approach is to cast the dynamic expression to a specified type if it is well-known.

### Example: dynamic member

In the following aspect, the logging aspect uses `meta.This`, which returns a `dynamic` object, to access the type being enhanced. The aspect assumes that the target type defines a field named `_logger` and that the type of this field has a method named `WriteLine`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicTrivial.cs name="meta.This"]

## Assignment of dynamic members

When the expression is writable, the `dynamic` member can be used on the right hand of an assignment:

```cs
// Translates into: this.MyProperty = 5;
meta.Property.Value = 5;
```

### Dynamic local variables

When the template is expanded, `dynamic` variables are transformed into `var` variables. Therefore, all `dynamic` variables must be initialized.

## Converting a dynamic expression into compile-time IExpression, and back

Under the hood, all `dynamic` values are compile-time objects implementing the <xref:Metalama.Framework.Code.IExpression> interface.

Whenever you have a `dynamic` and need compile-time <xref:Metalama.Framework.Code.IExpression> object, you can simply cast the `dynamic` into `IExpression`. Conversely, when you have an `IExpression` and want a run-time object, you simply have to use the `IExpression.Value` property.

Instead of using techniques like parsing to generate <xref:Metalama.Framework.Code.IExpression> objects, it can be convenient to write the expression in T#/C# and to convert it. This allows you to have expressions that depend on compile-time conditions and control flows.

For instance, suppose you want an `IExpression` that represents the `this` parameter for instance methods, or the first parameter for static methods. You can use the following code:

  ```cs
  var thisParameter = meta.Target.Method.IsStatic
                          ? meta.Target.Method.Parameters.First()
                          : (IExpression) meta.This;

  ```

You can now use `thisParameter` in an API that accepts an `IExpression` for instance:

```csharp
myMethod.Invoke( thisParameter );
```

You can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithType*> and <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithNullability*> extension methods to modify the return type of the returned <xref:Metalama.Framework.Code.IExpression>.
