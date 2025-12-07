---
uid: auxiliary-templates
level: 300
summary: "The document provides a detailed guide on using auxiliary templates in Metalama, including their creation, invocation, and usage in different scenarios like code reuse, abstraction, return statements, dynamic invocation of generic templates, and delegate-like invocation."
keywords: "Auxiliary templates, abstraction, Metalama, template invocation, generic templates, delegate-like invocation, statement factory"
created-date: 2023-12-11
modified-date: 2025-11-30
---

# Calling auxiliary templates

Auxiliary templates are templates designed to be called from other templates. When an auxiliary template is called from a template, the code it generates expands at the call site.

There are two primary reasons to use auxiliary templates:

* **Code reuse**: Moving repetitive code logic to an auxiliary template reduces duplication, aligning with Metalama's goal of streamlining code writing.
* **Abstraction**: Since template methods can be `virtual`, users of your aspects can customize templates.

You can call a template in two ways: the standard way, like calling any C# method, and the dynamic way, which addresses more advanced scenarios. Both approaches are covered in the following sections.

## Creating auxiliary templates

To create an auxiliary template, follow these steps:

1. Like a normal template, create a method and annotate it with the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute.

2. If you're creating this method outside of an aspect or fabric type, ensure the class implements the <xref:Metalama.Framework.Aspects.ITemplateProvider> empty interface.

    > [!NOTE]
    > This rule applies even if you want to create a helper class containing only `static` methods. In this case, you can't mark the class as `static`, but you can add a `private` constructor to prevent instantiation.

3. Most of the time, you'll want auxiliary templates to be `void`, as explained below.

A template can invoke another template just like any other method. You can pass values to its compile-time and run-time [parameters](xref:template-parameters).

> [!WARNING]
> An important limitation to bear in mind is that templates can be invoked only as _statements_ and not as part of an _expression_. We will revisit this restriction later in this article.

### Example: simple auxiliary templates

The following example is a simple caching aspect. The aspect is intended for use in different projects, and in some projects, we want to log a message on cache hit or miss. Therefore, we moved the logging logic to `virtual` auxiliary template methods with an empty implementation by default. In `CacheAndLog`, we override the logging logic.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate.cs]

## Using return statements in auxiliary templates

The behavior of `return` statements in auxiliary templates can sometimes be confusing compared to normal templates. Their nominal processing by the T# compiler is identical (the T# compiler doesn't differentiate auxiliary templates from normal templates; their difference is only in usage): `return` statements in any template result in `return` statements in the output.

In a normal non-void C# method, all execution branches must end with a `return <expression>` statement. However, because auxiliary templates often generate snippets instead of complete method bodies, you don't always want every branch of the auxiliary template to end with a `return` statement.

To work around this situation, make the auxiliary template `void` and call the <xref:Metalama.Framework.Aspects.meta.Return*?text=meta.Return> method, which generates a `return <expression>` statement while satisfying the C# compiler.

> [!NOTE]
> There's no way to explicitly interrupt the template processing other than playing with compile-time `if`, `else` and `switch` statements and ensuring that the control flow continues to the natural end of the template method.

### Example: meta.Return

The following example is a variation of our previous caching example, but we abstract the entire caching logic instead of just the logging part. The aspect has two auxiliary templates: `GetFromCache` and `AddToCache`. The first template is problematic because the cache hit branch must have a `return` statement while the cache miss branch must continue the execution. Therefore, we designed `GetFromCache` as a `void` template and used <xref:Metalama.Framework.Aspects.meta.Return*?text=meta.Return> to generate the `return` statement.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_Return.cs]

## Invoking generic templates

Auxiliary templates are beneficial when you need to call a generic API from a `foreach` loop and the type parameter must be bound to a type that depends on the iterator variable.

For instance, suppose you want to generate a field-by-field implementation of the `Equals` method and invoke the `EqualityComparer<T>.Default.Equals` method for each field or property of the target type. C# doesn't allow you to write `EqualityComparer<field.Type>.Default.Equals`, although this is what you'd conceptually need.

In this situation, use an auxiliary template with a [compile-time type parameter](xref:template-parameters).

To invoke the template, use <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> and specify the `args` parameter. For instance:

```cs
meta.InvokeTemplate(
     nameof(CompareFieldOrProperty),
     args: new { TFieldOrProperty = fieldOrProperty.Type, fieldOrProperty, other = (IExpression) other! } );
```

> [!TIP]
> Instead of using `nameof()` to reference a template, you can assign a stable identifier using the <xref:Metalama.Framework.Aspects.TemplateAttribute.Id> property and reference the template by that identifier. This is useful when templates are defined in a separate assembly where `nameof()` is not available.

This is illustrated by the following example:

### Example: invoking a generic template

The following aspect implements the `Equals` method by comparing all fields or automatic properties. For this exercise, we want to call the `EqualityComparer<T>.Default.Equals` method with the proper value of `T` for each field or property. This is achieved using an auxiliary template and the <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_StructurallyComparable.cs]

## Encapsulating a template invocation as a delegate

Calls to auxiliary templates can be encapsulated into an object of type <xref:Metalama.Framework.Aspects.TemplateInvocation>, similar to encapsulating a method call into a delegate. The <xref:Metalama.Framework.Aspects.TemplateInvocation> can be passed as an argument to another auxiliary template and invoked using the <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> method.

This technique is helpful when an aspect allows customizations of the generated code but the customized template must call given logic. For instance, a caching aspect may allow customization to inject `try..catch`, requiring a mechanism for the customization to call the desired logic inside the `try..catch`.

### Example: delegate-like invocation

The following code shows a base caching aspect named `CacheAttribute` that allows customizations to wrap the entire caching logic into arbitrary logic by overriding the `AroundCaching` template. This template must by contract invoke the <xref:Metalama.Framework.Aspects.TemplateInvocation> it receives. The `CacheAndRetryAttribute` uses this mechanism to inject retry-on-exception logic.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_TemplateInvocation.cs]

This example is contrived in two ways. First, it would make sense in this case to use two aspects. Second, using a `protected` method invoked by `AroundCaching` would be preferable. Using <xref:Metalama.Framework.Aspects.TemplateInvocation> makes sense when the template to call isn't part of the same class—for instance, if the caching aspect accepts options that can be set from a fabric, allowing users to supply a different implementation of this logic without overriding the caching attribute itself.

## Evaluating a template into an IStatement

If you want to use templates with facilities like <xref:Metalama.Framework.Code.SyntaxBuilders.SwitchStatementBuilder>, you'll need an <xref:Metalama.Framework.Code.SyntaxBuilders.IStatement>. To wrap a template invocation into an <xref:Metalama.Framework.Code.SyntaxBuilders.IStatement>, use <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.FromTemplate*?text=StatementFactory.FromTemplate>.

You can call <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.UnwrapBlock*> to remove braces from the template output, which will return an <xref:Metalama.Framework.Code.SyntaxBuilders.IStatementList>.

### Example: SwitchStatementBuilder

The following example generates an `Execute` method with two arguments: a message name and an opaque argument. The aspect must be used on a class with one or more `ProcessFoo` methods, where `Foo` is the message name. The aspect generates a `switch` statement that dispatches the message to the proper method. We use the <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.FromTemplate*?text=StatementFactory.FromTemplate> method to pass templates to the <xref:Metalama.Framework.Code.SyntaxBuilders.SwitchStatementBuilder>.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/SwitchStatementBuilder_FullTemplate.cs]

> [!div class="see-also"]
> <xref:template-overview>
> <xref:template-parameters>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.TemplateAttribute>
> <xref:Metalama.Framework.Aspects.TemplateInvocation>
> <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*>
> <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory>
