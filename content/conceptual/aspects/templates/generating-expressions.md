---
uid: run-time-expressions
level: 200
summary: "This document provides detailed information on generating run-time code in templates, using dynamic expressions and variables, invoking members, parsing C# expressions and statements, converting run-time expressions to compile-time, and converting compile-time values to run-time values."
keywords: "dynamic expressions, generating run-time code, compile-time values, run-time values, dynamic variables, IExpression.Value, ExpressionBuilder, dynamic member, parsing C# expressions"
created-date: 2023-02-21
modified-date: 2025-11-30
---

# Generating run-time expressions

In Metalama, expressions are compile-time objects that implement the `IExpression` interface.

Expressions represent C# syntax—not their result. For instance, `1+1` and `2` are two different expressions, although they evaluate to the same value at run time.

This article covers different ways to create `IExpression` objects.

## Two-way convertibility between IExpression and `dynamic`

As noted in <xref:dynamic-typing>, all `dynamic` objects in a template actually implement the `IExpression` interface, so it's safe to cast a `dynamic` into an `IExpression` in a template. An expression can be converted back to a `dynamic` either by using a cast or the `Value` property.

Therefore, `IExpression` objects are compile-time objects that represent run-time syntax:

- When typed as the compile-time `IExpression`, an expression can be used in compile-time APIs.
- When typed as `dynamic`, expressions can be used in run-time APIs.

## Capturing a C# expression into an IExpression

The simplest way to create an expression in a T# template is to write plain C# code and then capture its syntax. The `ExpressionFactory.Capture` method captures the C# syntax tree of an expression into an `IExpression` object without evaluating it.

```csharp
// Defines a run-time local variable.
var now = DateTime.Now;

// Captures the reference to the local variable "now".
var expression1 = ExpressionFactory.Capture( now );

// Captures the expression "DateTime.Now".
var expression2 = ExpressionFactory.Capture( DateTime.Now );
```

### Capturing a dynamic expression

> [!WARNING]
> When the compile-time type of the expression to capture is `dynamic`, it must be explicitly cast to `IExpression` to work around limitations of the C# language.

Example:

```csharp
// A compile-time object representing a method.
IMethod method;

// Invokes the method and stores the result in a run-time local variable.
// At compile time, `result` is dynamic.
var result = method.Invoke();

// Captures the reference to the local variable `result`.
// The `(IExpression)` cast is necessary.
var expression = ExpressionFactory.Capture( (IExpression) result );
```

## Generating expressions using a StringBuilder-like API

When you need to construct complex expressions programmatically or dynamically, the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class provides a text-based approach. It offers convenient methods like <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendLiteral*>, <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendTypeName*>, and <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendExpression*>. The <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendVerbatim*> method must be used for anything else, such as keywords or punctuation.

When you're done building the expression, call the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder.ToExpression*> method. It returns an <xref:Metalama.Framework.Code.IExpression> object. The <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property is `dynamic` and can be used in run-time code.

> [!NOTE]
> A major benefit of <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> is that it can be used in a compile-time method that isn't a template.

> [!WARNING]
> Your aspect must not assume that the target code has any required `using` directives. Make sure to write fully namespace-qualified type names. Metalama will simplify the code and add the relevant `using` directives when asked to produce pretty-formatted code. The best way to ensure type names are fully qualified is to use the <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendTypeName*> method.

### Example: ExpressionBuilder

The following example uses an <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> to build a pattern comparing an input value to several forbidden values. Notice the use of <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendLiteral*>, <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendExpression*>, and <xref:Metalama.Framework.Code.SyntaxBuilders.SyntaxBuilder.AppendVerbatim*>.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ExpressionBuilder.cs name="ExpressionBuilder"]

### Example: using ExpressionBuilder in BuildAspect

Since <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> works outside of templates, it's essential for generating expressions in contexts like <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*>. The following example introduces two fields: a static counter and an instance ID initialized using `Interlocked.Increment`. The initializer expression must be built programmatically because `BuildAspect` isn't a template.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ExpressionBuilderToExpression.cs name="ExpressionBuilder in BuildAspect"]

## Parsing string-based C# expressions

If you already have a string representing an expression or statement, you can convert it into an <xref:Metalama.Framework.Code.IExpression> using <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse>.

### Example: parsing expressions

The `_logger` field is accessed through a parsed expression in the following example.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ParseExpression.cs name="ParseExpression"]

## Generating run-time arrays

You can generate run-time arrays using two approaches:

### Using statements (multi-line approach)

The straightforward way is to declare an array variable and set each element using statements:

```cs
var args = new object[2];
args[0] = "a";
args[1] = DateTime.Now;
MyRunTimeMethod(args);
```

The limitation of this approach is that it requires multiple statements and can't be used where a single expression is needed.

### Using ArrayBuilder (expression approach)

To generate an array as a single _expression_, use the <xref:Metalama.Framework.Code.SyntaxBuilders.ArrayBuilder> class. This is particularly useful when passing arrays as method arguments or in other contexts where expressions are required.

For instance:

```cs
var arrayBuilder = new ArrayBuilder();
arrayBuilder.Add("a");
arrayBuilder.Add(DateTime.Now);
MyRunTimeMethod(arrayBuilder.ToValue());
```

This generates the following code:

```cs
MyRunTimeMethod(new object[] { "a", DateTime.Now });
```

## Generating interpolated strings

Instead of generating a string as an array separately and using `string.Format`, you can generate an interpolated string using the <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> class.

The following example shows how an <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> can be used to implement the `ToString` method automatically.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToString.cs name="ToString"]

> [!div id="parsing" class="anchor"]

## Converting compile-time values to run-time values

Use `meta.RunTime(expression)` to convert the result of a compile-time expression into a run-time expression. The compile-time expression is evaluated at compile time, and its value is converted into syntax representing that value. Conversions are possible for the following compile-time types:

- Literals
- Enum values
- One-dimensional arrays
- Tuples
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>
- <xref:System.Guid>
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>
- <xref:System.DateTime> and <xref:System.TimeSpan>
- Immutable collections: <xref:System.Collections.Immutable.ImmutableArray`1> and <xref:System.Collections.Immutable.ImmutableDictionary`2>
- Custom objects implementing the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface (see the "Converting custom objects" section below for details)

### Example: conversions

The following aspect converts the subsequent build-time values into a run-time expression: a `List<string>`, a `Guid`, and a `System.Type`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs name="Dynamic"]

### Converting custom objects

> [!div id="custom-conversion" class="anchor"]

You can have classes that exist both at compile-time and run-time. To allow Metalama to convert a compile-time value to a run-time value, your class must implement the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface. The <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder.ToExpression> method must generate a C# expression that, when evaluated, returns a value that's structurally equivalent to the current value. Note that your implementation of <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> isn't a template, so you'll need to use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class to generate your code.

### Example: custom converter

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CustomSyntaxSerializer.cs name="Custom Syntax Serializer"]

> [!div class="see-also"]
> <xref:run-time-statements>
> <xref:dynamic-typing>
> <xref:templates>
> <xref:Metalama.Framework.Code.IExpression>
> <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder>
> <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory>
> <xref:Metalama.Framework.Code.SyntaxBuilders.ArrayBuilder>
> <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder>
