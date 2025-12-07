---
uid: invokers
level: 300
summary: "Learn how to generate run-time code that invokes methods, accesses properties and fields, raises events, works with indexers, and creates tuple instances using the invoker API from the code model."
keywords: "invokers, IMethodInvoker, IExpression, method invocation, property access, field access, event handling, indexers, tuple creation, code generation"
created-date: 2025-11-07
modified-date: 2025-11-30
---

# Generating code based on the code model

When you have a <xref:Metalama.Framework.Code> representation of a declaration, you'll often want to access it from your generated run-time code. For instance, you'll often need to generate code that calls an <xref:Metalama.Framework.Code.IMethod> or accesses an <xref:Metalama.Framework.Code.IProperty>.

## What are invokers?

_Invokers_ are APIs that let you generate run-time code from compile-time declarations. When you write template code (which executes at compile-time), you use invokers to create compile-time expressions (<xref:Metalama.Framework.Code.IExpression>) that represent method calls, property accesses, and other operations. These expressions can then be used anywhere in your template and will be expanded into actual C# code when the template is applied.

The invoker functionality is implemented in the <xref:Metalama.Framework.Code.Invokers> namespace. The <xref:Metalama.Framework.Code.IMethod>, <xref:Metalama.Framework.Code.IFieldOrProperty>, <xref:Metalama.Framework.Code.IIndexer>, and <xref:Metalama.Framework.Code.IEvent> interfaces derive from <xref:Metalama.Framework.Code.Invokers.IMethodInvoker>, <xref:Metalama.Framework.Code.Invokers.IFieldOrPropertyInvoker>, <xref:Metalama.Framework.Code.Invokers.IIndexerInvoker>, and <xref:Metalama.Framework.Code.Invokers.IEventInvoker> respectively.

For scenarios where members are known at design time (when you write the aspect), you can also use the dynamic typing approach described in <xref:dynamic-typing>. Invokers provide more flexibility and control at compile time.

> [!WARNING]
> **The invoker API isn't type-safe.** Invokers will happily generate code that doesn't compile because of mismatched types. For example, you can call `method.Invoke("wrong", "types")` even if the method expects integers. The invoker API doesn't validate argument types or return types. Always verify that the code you generate matches the actual member signatures. The resulting invalid code will only be caught when the transformed code is compiled, confusing the aspect user.

## Calling a method

To generate an expression that represents the invocation of an <xref:Metalama.Framework.Code.IMethod>, use the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*?text=method.Invoke> method.

### Example: dynamic method invocation

In the following example, the `[CallAfter(methodName)]` aspect overrides the target method and calls a specified method after successful execution. The aspect author doesn't know which method will be called — this is determined by the aspect user when they apply the attribute. The aspect queries the code model to find the method by name, then uses the `Invoke` method to generate a call to it.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CallAfter.cs name="CallAfter"]

> [!NOTE]
> A production-ready aspect should validate that the method exists in `BuildAspect` and report a diagnostic error if it doesn't, rather than allowing the template to fail at compile time with an exception.

## Specifying the target object and nullability behavior

Before we go on with explaining the invoker API for other kinds of members, we must discuss a few options:

* **Target object (receiver)**. By default, when used with a non-static member, invokers generate calls for the current (`this`) instance. To specify a different instance, use the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithObject*?text=member.WithObject> method.
* **Nullability behavior**. By default, invokers use the `.` operator to access the member. If the target object is nullable, you might want to use `?.` instead. You can choose this behavior with the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*?text=member.WithOptions> method.

### Example

```csharp
IParameter p = meta.Target.Parameters[0];
var method = meta.Target.Type.Methods.OfName("Print").Single();

method.WithOptions( InvokerOptions.NullConditionalIfNullable ).WithObject( p ).Invoke( "Hello, world." );
```

Suppose that this template snippet is applied to a method with a nullable parameter:

```csharp
[SayHelloWorld]
void MyMethod( Printer? printer )
```

The template would generate the following code:

```csharp
printer?.Print( "Hello, world." );
```

Without <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithObject*?text=WithObject>, `this` would have been written instead of `printer`. Without <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*?text=WithOptions>, the simple dot `.` would have been generated instead of `?.`.

## Accessing a field or property

Fields and properties inherit the <xref:Metalama.Framework.Code.IExpression> interface. As with any expression, you can use the <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property to read or assign the field or property in a template. For fields, you can also use `ref` when accessing the `Value` property.

For instance:

```csharp
// Compile-time code querying the code model.
var targetProperty = meta.Target.Type.Properties["Target"];
var sourceProperty = meta.Target.Type.Properties["Source"];
var field = meta.Target.Type.Fields["TheField"];

// Referencing the properties in run-time code.
targetProperty.Value = sourceProperty.Value?.Trim();
SomeMethod( ref field.Value );
```

This generates the following code:

```csharp
Target = Source?.Trim();
SomeMethod( ref TheField );
```

## Accessing an event

Use <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Add*?text=event.Add>, <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Remove*?text=event.Remove>, or <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Raise*?text=event.Raise> to generate code that adds handlers to, removes handlers from, or raises an event.

## Working with indexers

You can access indexer items using the `this[ params object[] ]` or `this[ params IExpression[] ]` indexer of the <xref:Metalama.Framework.Code.Invokers.IIndexerInvoker> interface, which returns an <xref:Metalama.Framework.Code.IExpression>. This lets you access elements in a natural way.

For instance:

```csharp
var indexer = meta.Type.Indexers.Single();
indexer[0,0].Value += indexer[0,1].Value;
```

The template above generates the following code:

```csharp
this[0,0] = this[0,1]
```

## Working with tuples

### Creating a tuple instance

Use <xref:Metalama.Framework.Code.ITupleType.CreateCreateInstanceExpression*> to create a tuple instantiation expression.

For instance, in a template, you can use the following code:

```csharp
var tupleType = TypeFactory.CreateTupleType( (typeof(decimal), "Quantity"), (typeof(string), "ProductCode" ) );
var tupleInstance = tupleType.CreateCreateInstanceExpression(42, "HAT").Value;
```

This generates the following code:

```csharp
var tupleInstance = (Quantity: 42, ProductCode: "HAT");
```

You can also pass an array of <xref:Metalama.Framework.Code.IExpression> to <xref:Metalama.Framework.Code.ITupleType.CreateCreateInstanceExpression*> if the tuple items are known as compile-time expressions instead of C# expressions.

### Accessing tuple elements

Tuple elements are represented as fields in the tuple type. Use the following syntax to access their value:

```csharp
// Get the first element of a tuple
var firstElement = tupleType.TupleElements[0].WithObject( tupleInstance ).Value;
```

> [!div class="see-also"]
> <xref:dynamic-typing>
> <xref:templates>
> <xref:Metalama.Framework.Code.Invokers>
> <xref:Metalama.Framework.Code.Invokers.IMethodInvoker>
> <xref:Metalama.Framework.Code.Invokers.IFieldOrPropertyInvoker>
> <xref:Metalama.Framework.Code.Invokers.IEventInvoker>
> <xref:Metalama.Framework.Code.Invokers.IIndexerInvoker>
