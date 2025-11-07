---
uid: run-time-expressions
level: 200
summary: "This document provides detailed information on generating run-time code in templates, using dynamic expressions and variables, invoking members, parsing C# expressions and statements, converting run-time expressions to compile-time, and converting compile-time values to run-time values."
keywords: "dynamic expressions, generating run-time code, compile-time values, run-time values, dynamic variables, IExpression.Value, ExpressionBuilder, dynamic member, parsing C# expressions"
created-date: 2023-02-21
modified-date: 2024-11-06
---

# Generating run-time expressions

In Metalama, expressions are compile-time objects that implement the `IExpression` interface. 

Expressions represent C# syntax - not their result. For instance, `1+1`  and `2` are two different expressions, althought they evaluate to the same value at run time.

In this article, we cover different ways to create `IExpression` objects.


## Two-way conversibility between IExpression and `dynamic`

As noted in <xref:dynamic-typing>, all `dynamic` objects in a template actually implement the `IExpression` interface, so it is safe to case a `dynamic` into an `IExpression` in a template. An expression can be converted back to a `dynamic` either using a cast, either the `Value` property.

Therefore, `IExpression` objects are compile-time objects that represent run-time syntax:
- When typed as the compile-time `IExpression`, expression can be used in compile-time APIs.
- When typed as `dynamic`, expressions can be used in run-time APIs.

## Capturing a C# expression into an IExpression

The simplest way to write an expression in a T# template is to write plain C# code. You can use then `ExpressionFactory.Capture` method to capture the C# syntax into an `IExpression` object.

```csharp
var now = DateTime.Now; // Defines a run-time local varaible.
var expression1 = ExpressionFactory.Capture( now ); // Captures the reference to the local variable "now".
var expression2 = ExpressionFactory.Capture( DateTime.Now ); // Captures the expression "DateTime.Now".
```

### Capturing a dynamic expression

> [!WARNING]
> When the expression to capture is of `dynamic` type, it must be explicitk cast to `IExpression` to work around limitations of the C# language.

Example:

```csharp
IMethod method; // A compile-time object representing a method.
var result = method.Invoke(); // Defines a run-time local variable assigned to the method invocation return value;
var expression = ExpressionFactory.Capture( (IExpression) result ); // Captures the reference to the local variable "result".
```

## Generating expressions using a StringBuilder-like API

It is sometimes easier to generate the run-time code as simple text instead of using a complex meta API. In this situation, you can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class. It offers convenient methods like <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendLiteral*>, <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendTypeName*>, or <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendExpression*>. The <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendVerbatim*> method must be used for anything else, such as keywords or punctuation.

When you are done building the expression, call the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder.ToExpression*> method. It will return an <xref:Metalama.Framework.Code.IExpression> object. The <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property is `dynamic` and can be used in run-time code.

> [!NOTE]
> A major benefit of <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> is that it can be used in a compile-time method that is not a template.

> [!WARNING]
> Your aspect must not assume that the target code has any required `using` directives. Make sure to write fully namespace-qualified type names. Metalama will simplify the code and add the relevant `using` directives when asked to produce pretty-formatted code. The best way to ensure type names are fully qualified is to use the <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendTypeName*> method.

### Example: ExpressionBuilder

The following example uses an <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> to build a pattern comparing an input value to several forbidden values. Notice the use of <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendLiteral*>, <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendExpression*>, and <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendVerbatim*>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ExpressionBuilder.cs name="ExpressionBuilder"]

## Parsing string-based C# expressions

If you already have a string representing an expression or a statement, you can turn it into an <xref:Metalama.Framework.Code.IExpression> using the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse>.

### Example: parsing expressions

The `_logger` field is accessed through a parsed expression in the following example.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ParseExpression.cs name="ParseExpression"]


## Generating run-time arrays

The first way to generate a run-time array is to declare a variable of array type and use a statement to set each element, for instance:

```cs
var args = new object[2];
args[0] = "a";
args[1] = DateTime.Now;
MyRunTimeMethod(args);
```

The problem of this approach is that it requires several statements.

To generate an array as an _expression_, you can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ArrayBuilder> class.

For instance:

```cs
var arrayBuilder = new ArrayBuilder();
arrayBuilder.Add("a");
arrayBuilder.Add(DateTime.Now);
MyRunTimeMethod(arrayBuilder.ToValue());
```

This will generate the following code:

```cs
MyRunTimeMethod(new object[] { "a", DateTime.Now });
```

## Generating interpolated strings

Instead of generating a string as an array separately and using `string.Format`, you can generate an interpolated string using the <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> class.

The following example shows how an <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> can be used to implement the `ToString` method automatically.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToString.cs name="ToString"]

> [!div id="parsing" class="anchor"]

## Converting compile-time values to run-time values

You can utilize `meta.RunTime(expression)` to convert the result of a compile-time expression into a run-time expression. The compile-time expression will be evaluated at compile time, and its value will be converted into syntax representing that value. Conversions are possible for the following compile-time types:

- Literals;
- Enum values;
- One-dimensional arrays;
- Tuples;
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>;
- <xref:System.Guid>;
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>;
- <xref:System.DateTime> and <xref:System.TimeSpan>;
- Immutable collections: <xref:System.Collections.Immutable.ImmutableArray`1> and <xref:System.Collections.Immutable.ImmutableDictionary`2>;
- Custom objects implementing the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface (see [Converting custom objects from compile-time to run-time values](#custom-conversion) for details).

### Example: conversions

The following aspect converts the subsequent build-time values into a run-time expression: a `List<string>`, a `Guid`, and a `System.Type`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs name="Dynamic"]

### Converting custom objects

> [!div id="custom-conversion" class="anchor"]

You can have classes that exist both at compile and run time. To allow Metalama to convert a compile-time value to a run-time value, your class must implement the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface. The <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder.ToExpression> method must generate a C# expression that, when evaluated, returns a value that is structurally equivalent to the current value. Note that your implementation of <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> is _not_ a template, so you will have to use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class to generate your code.

### Example: custom converter

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CustomSyntaxSerializer.cs name="Custom Syntax Serializer"]

