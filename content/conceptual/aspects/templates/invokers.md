---
uid: invokers
level: 300
summary: "Learn how to generate run-time code that invokes methods, accesses properties and fields, raises events, works with indexers, and creates tuple instances using the invoker API from the code model."
keywords: "invokers, IMethodInvoker, IExpression, method invocation, property access, field access, event handling, indexers, tuple creation, code generation"
created-date: 2025-11-07
modified-date: 2025-11-07
---


# Generating code based on the code model

When you have a <xref:Metalama.Framework.Code> representation of a declaration, you will often want to access it from your generated run-time code. For instance, you will often need to generate code that calls an <xref:Metalama.Framework.Code.IMethod>, or accesses an <xref:Metalama.Framework.Code.IProperty>. 

Technically speaking, you will generate compile-time expressions (<xref:Metalama.Framework.Code.IExpression>) that represent the method call, property access, and so on, and you can the use the <xref:Metalama.Framework.Code.IExpression> anywhere in a template. This feature is implemented in the <xref:Metalama.Framework.Code.Invokers> namespace.


## Calling a method

To generate an expression that represents the invocation of an <xref:Metalama.Framework.Code.IMethod>, use the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.Invoke*?text=method.Invoke> method to generate code that invokes a method.

### Example: invoking members

The following example is a variation of the previous one. The aspect no longer assumes the logger field is named `_logger`. Instead, it looks for any field of type `TextWriter`. Because it does not know the field's name upfront, the aspect must use the <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property to get an expression allowing it to access the field. This property returns a `dynamic` object, but we cast it to `TextWriter` because we know its actual type. When the template is expanded, Metalama recognizes that the cast is redundant and simplifies it. However, the cast is useful in the T# template to get as much strongly-typed code as we can.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicCodeModel.cs name="Invokers"]


## Setting the object and nullabilty access

Before we go on with explaining invoker API for other kinds of members, we must discuss a few options.

* **Target object (receiver)**. By default, when used with a non-static member, all the methods and properties above generate calls for the current (`this`) instance. To specify a different instance, use the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithObject*?text=member.WithObject> method.
* **Nullability behavior**. By default, invokers use the `.` operator to access the member. If the receiver is nullable, you might want to use `?.` instead. You can choose this behavior with the <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.WithOptions*?text=member.WithOptions> method.

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

Fields and properties inherit the <xref:Metalama.Framework.Code.IExpression> interface. As with any expression, you can use the <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> property to read or assign the field or property in a template. With fields, you can also reference the `Value` property with `ref`.

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

This will generate the following code

```csharp
Target = Source;
SomeMethod( ref TheField );
```

## Accessing an event

Use the <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Add*?text=event.Add>, <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Remove*?text=event.Remove>, or <xref:Metalama.Framework.Code.Invokers.IEventInvoker.Raise*?text=event.Raise> to generate code that interacts with an event.


## Working with indexers

You can access indexer items using the `this[ params object[] ]` or `this[ params IExpression[] ]` indexer of the <xref:Metalama.Framework.Code.Invokers.IIndexerInvoker> interface, which returns an <xref:Metalama.Framework.Code.IExpression>. This allows you to access elements in a natural way.

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
var tupleInstance = tupleType.CreateCreateInstanceExpression(42, "HAT").Value;
```

This will generate the following code:

```csharp
var tupleInstance = (Quantity: 42, ProductCode: hat);
```

You can also pass an array of <xref:Metalama.Framework.Code.IExpression> to <xref:Metalama.Framework.Code.ITupleType.CreateCreateInstanceExpression*> if the tuple items are known as compile-time expressions instead of C# expressions.

### Accessing tuple elements

Tuple elements act as fields of the <xref:System.ValueTuple> type. Use the following syntax to access their value:

```csharp
// Get the first element of a tuple
var firstElement = tupleType.TupleElements[0].WithObject( tupleInstance ).Value;
```

