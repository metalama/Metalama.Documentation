---
uid: main-getting-started
keywords: "Metalama, getting started"
created-date: 2024-03-19
modified-date: 2024-08-22
---

# Getting started with Metalama

> [!NOTE]
> If you don't plan to create your own aspects but just use existing ones, start with <xref:using-metalama>.


## 1. Add Metalama to Your Project

Add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package to your project.

> [!NOTE]
> If your project targets the .NET Framework or .NET Standard, you may also need to add [PolySharp](https://github.com/Sergio0694/PolySharp), which updates the language version even if it's officially unsupported.

Optionally, install [Visual Studio Tools for Metalama](https://www.postsharp.net/links/download-unified-vsx). This extension offers the following features:

* AspectDiff: Displays a side-by-side comparison of source code with the generated code.
* CodeLens: Displays which aspects are applied to your code.
* Syntax highlighting of aspects: This is particularly useful when you are getting started.

## 2. Create an Aspect Class

Let's start with logging, the traditional _Hello, world_ example of aspect-oriented programming.

Type the following code:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted.Aspect.cs]

As you can infer from its name, the `LogAttribute` class is a custom attribute. You can think of an aspect as a _template_. When you apply it to some code (in this case, to a method), it transforms it. Indeed, the code of the target method will be replaced by the implementation of `OverrideMethod`. This method is very special. Some parts execute at run time, while others, which typically start with the `meta` keyword, execute at compile time. If you installed Visual Studio Tools for Metalama, you will notice that compile-part segments are displayed with a different background color.

Let's examine two `meta` expressions:

* `meta.Proceed()` is replaced by the code of the target method.
* `meta.Target.Method` gives you access to the <xref:Metalama.Framework.Code.IMethod> code model. In this case, we are implicitly calling `ToString()`.

## 4. Apply the custom attribute to a method

Remember that an aspect is a template and that it doesn't do anything until it's applied to some target code.

So, let's add the `[Log]` attribute to some method:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted.cs]

Now, if you execute the method, the following output is printed:

```text
Entering Foo.Method1()
Hello, world.
Leaving Foo.Method1()
```

## 5. See what happened to your code

You can see that Metalama did not modify anything in your source code. It's still _yours_. Instead, Metalama applied the logging aspect during compilation. So, it's no longer your source code that's being executed, but your source code _enhanced_ by the logging aspect.

If you installed Visual Studio Tools for Metalama, you can compare your source code with the transformed (executed) code using the "Diff preview" feature accessible from the source file context menu in Visual Studio.

It will show you something like this:

[!metalama-compare ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted.cs]

## 6. Add aspects in bulk using fabrics

With aspects like logging, it's frequently applied to a large number of methods. It would be cumbersome to add a custom attribute to each of them. Instead, let's see how we can add the aspect programmatically using fabrics.

Use the following code:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted_Fabric.Fabric.cs]

This class derives from <xref:Metalama.Framework.Fabrics.ProjectFabric> and acts as a compile-time entry point for the project. As you can see, it adds the logging aspect to all public methods of all public types.

## 6. Add architecture validation

Now that you know about aspects and fabrics, it's easy to understand how to validate your codebase against some architectural rules. In this example, we will show how to report a warning when internals of a namespace are used outside of this namespace.

First, reference the [Metalama](https://www.nuget.org/packages/Metalama.Extensions.Architecture) package from your project.

Then, add a fabric with the validation logic. We can use a <xref:Metalama.Framework.Fabrics.ProjectFabric> as above:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted_Architecture.Fabric.cs]

Alternatively, we can achieve the same with a <xref:Metalama.Framework.Fabrics.NamespaceFabric>, which acts within the scope of their namespace instead of their project:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted_Architecture_Ns.Fabric.cs]

Fabrics not only run at compile time, but also at design time within the IDE. After the first build, or after you click on the _I am done with compile-time changes_ link if you have installed Metaslama Tools for Visual Studio, you will see warnings in the IDE if your code violates the rule.

In this case, when we try to access any class of `VerifiedNamespace` from a different namespace, we get a warning:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/GettingStarted/GettingStarted_Architecture.cs]

## Conclusion

Congratulations! In this short tutorial, you have discovered two key concepts of Metalama: aspects and fabrics. You have learned how to transparently add behaviors to your code during compilation, and add validation rules that get enforced in real time in the editor.

There are three paths you can take from here according to your learning style:

* <xref:videos>
* <xref:conceptual>
* <xref:samples>


