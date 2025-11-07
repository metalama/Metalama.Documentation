---
uid: run-time-statements
level: 200
summary: "Learn how to dynamically generate run-time statements in templates using StatementBuilder, parse string-based statements, define local variables programmatically, and build switch statements."
keywords: "statements, StatementBuilder, meta.InsertStatement, meta.DefineLocalVariable, SwitchStatementBuilder, dynamic code generation, parsing statements"
created-date: 2023-02-21
modified-date: 2025-11-07
---

# Generating run-time statements

In T# templates, statements are instructions that perform actions in the generated code, such as variable declarations, assignments, method calls, loops, and conditionals.

Most of the time, you can write statements directly in your template, and Metalama uses inference to automatically detect whether a statement should execute at compile-time or be included in the generated run-time code. This approach works transparently in most cases. However, when you need to dynamically generate statements based on compile-time logic—such as building a `switch` statement with a variable number of cases—you need to use special APIs.

To dynamically add statements to the generated code, use <xref:Metalama.Framework.Aspects.meta.InsertStatement*>, which accepts an `IStatement` or `IExpression` object (since most C# expressions can also be used as statements).

In this article, we'll see how to dynamically add statements to the generated code.

## Generating statements using a StringBuilder-like API

When you need to construct statements programmatically or generate complex statements dynamically, use the <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder> class. This is the statement equivalent of <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder>. It allows you to generate both individual statements and _blocks_ of statements using its <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder.BeginBlock*> and <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder.EndBlock*> methods.


> [!WARNING]
> Do not forget the trailing semicolon at the end of the statement.

When you are done, call the <xref:Metalama.Framework.Code.SyntaxBuilders.IStatementBuilder.ToStatement*> method. You can inject the returned <xref:Metalama.Framework.Code.SyntaxBuilders.IStatement> in run-time code by calling the <xref:Metalama.Framework.Aspects.meta.InsertStatement*> method in the template.

## Parsing C# statements

Just as you can parse C# expressions using <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse>, you can parse statements from strings using the <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.Parse*?text=StatementFactory.Parse> method.


> [!WARNING]
> Do not forget the trailing semicolon at the end of the statement.

## Defining local variables

In T# templates, local variable declarations are automatically classified as either compile-time or run-time according to the value they are assigned:

- Variables assigned to compile-time values (like `var field = meta.Target.Field;`) are compile-time variables.
- Variables assigned to run-time expressions (like `var x = 0;`) become run-time local variables in the generated code.

However, when you need to _programmatically_ define local variables — for example, creating a variable within a compile-time `foreach` loop — you must use the <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*> method. This method allows dynamic creation of local variables whose number or names are determined at compile-time.

When using <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*>, you don't need to worry about generating unique names. Metalama automatically appends a numerical suffix to the variable name to ensure uniqueness within the target lexical scope.

### Example: rolling back field changes upon exception

The following aspect saves the value of all fields and automatic properties into a local variable before an operation is executed and rolls back these changes upon exception.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ExpressionBuilder.cs name="ExpressionBuilder"]

## Generating switch statements

You can use the <xref:Metalama.Framework.Code.SyntaxBuilders.SwitchStatementBuilder> class to generate `switch` statements. Note that it is limited to _constant_ and _default_ labels, i.e., patterns are not supported. Tuple matching is supported.

### Example: SwitchStatementBuilder

The following example generates an `Execute` method, which has two arguments: a message name and an opaque argument. The aspect must be used on a class with one or more `ProcessFoo` methods, where `Foo` is the message name. The aspect generates a `switch` statement that dispatches the message to the proper method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SwitchStatementBuilder.cs name="SwitchStatementBuilder"]

## See also

For generating expressions (rather than statements), see <xref:run-time-expressions>, which covers expression builders, parsing expressions, and converting compile-time values to run-time values.
