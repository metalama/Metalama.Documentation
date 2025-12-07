---

uid: template-compile-time
level: 200
summary: "This document provides detailed information on writing compile-time code using the Metalama Framework, including compile-time expressions, statements, the 'meta' pseudo-keyword, compile-time language constructs, aspect properties, compile-time types and methods, and calling other packages from compile-time code."
keywords: "compile-time code, Metalama Framework, compile-time expressions, meta pseudo-keyword, aspect properties, compile-time types, compile-time methods, compile-time variables, compile-time statements, compile-time constructs"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Writing compile-time code

Compile-time expressions are expressions that contain a call to a compile-time method, a reference to a compile-time local variable, or a compile-time aspect member. These expressions are executed at compile time when the aspect is applied to a target.

Compile-time statements, such as `if`, `foreach`, or `meta.DebugBreak()`, are also executed at compile time.

## The meta pseudo-keyword

The <xref:Metalama.Framework.Aspects.meta> static class serves as the entry point for the compile-time API. It can be considered a pseudo-keyword that provides access to the _meta_ side of meta-programming. The <xref:Metalama.Framework.Aspects.meta> class is the entry point to the meta-model, and its members can only be invoked within the context of a template.

The <xref:Metalama.Framework.Aspects.meta> static class exposes the following members:

- The <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> method, which invokes the target method or accessor being intercepted. This can be either the next aspect applied on the same target or the target source implementation itself.
- The <xref:Metalama.Framework.Aspects.meta.Target?text=meta.Target> property, which provides access to the declaration to which the template is applied.
- The <xref:Metalama.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters> property, which provides access to the current method or accessor parameters.
- The <xref:Metalama.Framework.Aspects.meta.This?text=meta.This> property, which represents the `this` instance. Used in conjunction with <xref:Metalama.Framework.Aspects.meta.Base?text=meta.Base>, <xref:Metalama.Framework.Aspects.meta.ThisType?text=meta.ThisType>, and <xref:Metalama.Framework.Aspects.meta.BaseType?text=meta.BaseType> properties, `meta.This` enables your template to access members of the target class using dynamic code.
- The <xref:Metalama.Framework.Aspects.meta.Tags?text=meta.Tags> property, which provides access to an arbitrary dictionary passed to the advice factory method. See <xref:sharing-state-with-advice> for details.
- The <xref:Metalama.Framework.Aspects.meta.CompileTime*?text=meta.CompileTime> method, which coerces a neutral expression into a compile-time expression.
- The <xref:Metalama.Framework.Aspects.meta.RunTime*?text=meta.RunTime> method, which converts the result of a compile-time expression into a run-time value.

### Example: simple logging

The following code writes a message to the system console before and after the method execution. The text includes the name of the target method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleLogging.cs name="Simple Logging"]

## Compile-time language constructs

### Compile-time local variables

Local variables are run-time by default. To declare a compile-time local variable, you must initialize it with a compile-time value. If you need to initialize the compile-time variable with a literal value such as `0` or `"text"`, use the `meta.CompileTime` method to convert the literal into a compile-time value.

Examples:

- In `var i = 0;`, `i` is a run-time variable.
- In `var i = meta.CompileTime(0);`, `i` is a compile-time variable.
- In `var parameters = meta.Target.Parameters;`, `parameters` is a compile-time variable.

> [!NOTE]
> A compile-time variable can't be assigned from a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch`, or `finally`.

### Compile-time if

If the condition of an `if` statement is a compile-time expression, the `if` statement will be interpreted at compile-time.

#### Example: if

In the following example, the aspect prints a different string for static methods than for instance methods.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeIf.cs name="Compile-Time If"]

### Compile-time foreach

If the expression of a `foreach` statement is a compile-time expression, the `foreach` statement will be interpreted at compile-time.

#### Example: foreach

The following aspect uses a `foreach` loop to print the value of each parameter of the method to which it's applied.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeForEach.cs name="Compile-Time If"]

### No compile-time for and goto

Compile-time `for` loops aren't supported. `goto` statements are also not allowed in templates. If you need a compile-time `for`, you can use the following construct:

```cs
foreach ( int i in meta.CompileTime( Enumerable.Range( 0, n ) ) )
```

If the above approach isn't feasible, you can move your logic to a compile-time aspect function (not a template method), have this function return an enumerable, and use the return value in a `foreach` loop in the template method.

### nameof expressions

`nameof` expressions in compile-time code are always pre-compiled into compile-time expressions, enabling compile-time code to reference run-time types.

### typeof expressions

When `typeof(Foo)` is used with a run-time-only type `Foo`, a mock `System.Type` object is returned. This object can be used in run-time expressions or as an argument of Metalama compile-time methods. However, most members of this fake `System.Type` _can't_ be evaluated at compile time and will throw an exception. In some cases, you may need to call the <xref:Metalama.Framework.Aspects.meta.RunTime*?text=meta.RunTime> method to indicate to the T# compiler that you want a run-time expression instead of a compile-time one.

## Aspect properties

Many aspects have properties that can be set when the aspect is instantiated — for instance as a custom attribute. The scope of these properties is generally run-time-or-compile-time. When you read these properties from a template, they'll be replaced by their compile-time value.

### Example: aspect property

The following example shows a simple _Retry_ aspect. The maximum number of attempts can be configured by setting a property of the custom attribute. This property is compile-time. As you can see, when the template is expanded, the property reference is replaced by its value.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Retry.cs name="Retry"]

## Compile-time types and methods

If you want to share compile-time code between aspects or aspect methods, you can create your own types and methods that execute at compile time.

- Compile-time code must be annotated with the [<xref:Metalama.Framework.Aspects.CompileTimeAttribute?text=CompileTime>] custom attribute. This attribute is typically used on:
  - A method or field of an aspect
  - A type (`class`, `struct`, `record`, ...)
  - The whole project, using `[assembly: CompileTime]`
- Code that can execute at either compile or run time must be annotated with the [<xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute?text=RunTimeOrCompileTime>] custom attribute.

## Calling other packages from compile-time code

By default, compile-time code can only call the following APIs:

- .NET Standard 2.0 (all libraries)
- Metalama.Framework

For advanced scenarios, the following packages are also included by default:

- Metalama.Framework.Sdk
- Microsoft.CodeAnalysis.CSharp

To make another package available in compile-time code:

1. Make sure that this package targets .NET Standard 2.0.
2. Ensure that the package is included in the project.
3. Edit your `.csproj` or `Directory.Build.props` file and add the following:

```xml
<ItemGroup>
 <MetalamaCompileTimePackage Include="MyPackage"/>
</ItemGroup>
```

Once this configuration is done, `MyPackage` can be used both in run-time and compile-time code.

> [!WARNING]
> You must also specify `MetalamaCompileTimePackage` in each project that _uses_ the aspects.

> [!div class="see-also"]
> <xref:template-overview>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.meta>
> <xref:Metalama.Framework.Aspects.CompileTimeAttribute>
> <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute>
