---
uid: simple-override-method
level: 200
summary: "The document provides a guide on how to override methods using the Metalama.Framework, with examples including logging, retrying upon exception, authorizing users, including method names in logs, and profiling methods."
keywords: "overriding methods, Metalama.Framework, logging, retry upon exception, authorizing users, method names in logs, profiling methods, OverrideMethodAspect, meta.Proceed, meta.Target.Method"
created-date: 2023-02-28
modified-date: 2024-08-04
---

# Getting started: overriding a method

Overriding a method is one of the simplest aspects you can implement. Your aspect's implementation will replace the original implementation. Let's discuss how this works.

## Creating your first method aspect

To create an aspect that overrides methods, follow these steps:

1. Add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package to your project.

2. Create a class and inherit the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> class. This class will serve as a custom attribute, so it's recommended to name it with the `Attribute` suffix.

3. Override the <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod*> method.

4. In your <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod*> implementation, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> where the original method should be invoked.

    > [!NOTE]
    > <xref:Metalama.Framework.Aspects.meta> is a unique class. It can almost be considered a keyword that allows you to interact with the meta-model of the code you are working with. In this case, calling <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> is equivalent to calling the method that your aspect is overriding.

5. Apply your new aspect to any relevant method as a custom attribute.

### Example: trivial logging

The following aspect overrides the target method and adds a call to `Console.WriteLine` to write a message before the method is executed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods\SimpleLogging.cs]

To see the effect of the aspect on the code in this documentation, switch to the _Transformed Code_ tab above. In Visual Studio, you can preview the transformed code using the _Diff Preview_ feature. Refer to <xref:understanding-your-code-with-aspects> for details.

As demonstrated, <xref:Metalama.Framework.Aspects.OverrideMethodAspect> does exactly what the name suggests: it overrides the method. If you apply your aspect to a method, the aspect code will be executed _instead_ of the target method's code. Therefore, the following line of code is executed first:

```csharp
Console.WriteLine($"Simply logging a method..." );
```

Then, thanks to the call to <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>, the original method code is executed.

Admittedly, this aspect does not do much yet. Let's make it more useful.

### Example: retrying upon exception

In the previous chapter, you used the built-in aspect `Retry`. Here is its implementation.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Retry.cs]

Notice how the overridden implementation in the aspect retries the method being overridden. In this example, the number of retries is hard-coded.

### Example: authorizing the current user

The following example demonstrates how to verify the current user's identity before executing a method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Authorize.cs]

## Adding context from the target method

None of the above examples contain anything specific to the method to which the aspect was applied. Even the logging aspect wrote a generic message.

Instead of writing a generic message to the console, let's write a text that includes the name of the target method.

You can access the target of the aspect by calling the <xref:Metalama.Framework.Aspects.IMetaTarget.Method?text=meta.Target.Method> property, which exposes all relevant information about the current method: its name, its list of parameters and their types, etc.

To get the name of the method you are targeting from the aspect code, call <xref:Metalama.Framework.Code.INamedDeclaration.Name?text=meta.Target.Method.Name>. You can get the qualified name of the method by calling the `meta.Target.Method.ToDisplayString()` method.

Let's see how this information can be used to enhance the logging aspect we've already created.

The following code demonstrates how this can be done:

### Example: including the method name in the log

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Log.cs]

### Example: profiling a method

When you need to find out which method call is taking time, the first step is usually to decorate the method with print statements to determine how much time each call takes. The following aspect allows you to wrap that in an aspect. Whenever you need to track the calls to a method, just apply this aspect (in the form of the attribute) to the method as shown in the Target code.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Profile.cs]

## Going deeper

If you want to go deeper into method overrides, consider reading the following articles:

* In this article, you have learned how to use `meta.Proceed` and `meta.Target.Method.Name` in your templates. You can create much more complex and powerful templates, even doing compile-time `if` and `foreach` blocks. To learn how, jump directly to <xref:templates>.

* To learn how to have different templates for `async` or iterator methods, or to learn how to override several methods from a single type-level aspect, jump to <xref:overriding-methods>.

> [!div class="see-also"]
> <xref:simple-override-method>





