---
uid: run-time-statements
level: 200
summary: ""
keywords: ""
created-date: 2023-02-21
modified-date: 2024-11-06
---

# Generating run-time statements

Statements in C# are much simpler than expressions.

In a T# template, Metalama uses inferences to detect if a template statement is a compile-time one, or a run-time one. This works transparently most of the time, but notably fails when you need to dynamically generate, for instance, a `switch` statement.

In these cases, you will need to add a statement dynamically to the generated code. This can be done using the `meta.InsertStatement`, which requires an `IStatement` or `IExpression` object (as most C# expressions can also be used as statements),

In this article, we'll see how to dynamically add statements to the generated code.

## Generating statements using a StringBuilder-like API

<xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder> is to statements what <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> is to expressions. Note that it also allows you to generate _blocks_ thanks to its <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder.BeginBlock*> and <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder.EndBlock*> methods.

> [!WARNING]
> Do not forget the trailing semicolon at the end of the statement.

When you are done, call the <xref:Metalama.Framework.Code.SyntaxBuilders.IStatementBuilder.ToStatement*> method. You can inject the returned <xref:Metalama.Framework.Code.SyntaxBuilders.IStatement> in run-time code by calling the <xref:Metalama.Framework.Aspects.meta.InsertStatement*> method in the template.

## Parsing C# statements

Just as you can parse C# expressions using <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse>, you can parse a statement using <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.Parse*?text=StatementFactory.Parse> method.

> [!WARNING]
> Do not forget the trailing semicolon at the end of the statement.

### Example: parsing expressions

The `_logger` field is accessed through a parsed expression in the following example.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ParseExpression.cs name="ParseExpression"]

## Defining local variables

By default, local variables of your T# template represent a run-time local variable unless they are assigned to a build-time value. For instance, `var x = 0;` defines a run-time local variable and `var field = meta.Target.Field;` defines a compile-time one.

If you need to _dynamically_ define a local variable, you can use the <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*> method. This allows you, for instance, to define local variables in a compile-time `foreach` loop.

When using the <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*> method, you should not worry about generating unique names. Metalama will append a numerical suffix to the variable name to ensure it is unique in the target lexical scope.

### Example: rollbacking field changes upon exception

The following aspect saves the value of all fields and automatic properties into a local variable before an operation is executed and rolls back these changes upon exception.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ExpressionBuilder.cs name="ExpressionBuilder"]

## Generating switch statements

You can use the <xref:Metalama.Framework.Code.SyntaxBuilders.SwitchStatementBuilder> class to generate `switch` statements. Note that it is limited to _constant_ and _default_ labels, i.e., patterns are not supported. Tuple matching is supported.

### Example: SwitchStatementBuilder

The following example generates an `Execute` method, which has two arguments: a message name and an opaque argument. The aspect must be used on a class with one or many `ProcessFoo` methods, where `Foo` is the message name. The aspect generates a `switch` statement that dispatches the message to the proper method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SwitchStatementBuilder.cs name="SwitchStatementBuilder"]

