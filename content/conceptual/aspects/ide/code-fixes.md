---
uid: code-fixes
level: 300
summary: "The document provides detailed instructions on how to offer code fixes and refactorings using the Metalama Framework, including attaching code fixes to diagnostics, suggesting refactorings without diagnostics, and building multi-step code fixes. It also discusses performance considerations."
keywords: "code fixes, refactorings, Metalama Framework, diagnostics, code fix suggestions, multi-step code fixes, CodeFixFactory, ScopedDiagnosticSink, ICodeActionBuilder, performance considerations"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Offering code fixes and refactorings

## Attaching code fixes to diagnostics

When an aspect or fabric reports a diagnostic, it can attach a set of code fixes to this diagnostic by invoking the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> extension method. The <xref:Metalama.Extensions.CodeFixes.CodeFixFactory> class can be used to create single-step code fixes.

## Suggesting code refactorings without diagnostics

An aspect or fabric can also suggest a code refactoring without reporting a diagnostic by invoking the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method.

### Example

The example below demonstrates an aspect that implements the `ToString` method. By default, it includes all public properties of the class in the `ToString` result. However, the developer using the aspect can opt-out by adding `[NotToString]` to any property.

The aspect utilizes the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method to add a code fix suggestion for all properties not yet annotated with `[NotToString]`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithSimpleCodeFix.cs name="ToString aspect with simple code fix"]

## Building multi-step code fixes

To create a custom code fix, instantiate the <xref:Metalama.Extensions.CodeFixes.CodeFix> class using the constructor instead of the <xref:Metalama.Extensions.CodeFixes.CodeFixFactory> class.

The <xref:Metalama.Extensions.CodeFixes.CodeFix> constructor accepts two arguments:

* The _title_ of the code fix, which will be displayed to the user, and
* A _delegate_ of type `Func<ICodeActionBuilder, Task>` which will apply the code fix when the user selects it

The title must be globally unique for the target declaration. Even two different aspects cannot provide two code fixes with the same title to the same declaration.

The delegate will typically utilize one of the following methods of the <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder> interface:

| Method | Description |
|------|----|
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.AddAttributeAsync*> | Adds a custom attribute to a declaration.
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.RemoveAttributesAsync*> | Removes all custom attributes of a given type from a given declaration and all contained declarations.
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> | Transforms the source code using an aspect (as if it were applied as a live template).

### Example

The previous example is continued here, but instead of a single-step code fix, we want to offer the user the ability to switch from an aspect-oriented implementation of `ToString` by applying the aspect to the source code itself.

The custom code fix performs the following actions:

* Applies the aspect using the <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> method.
* Removes the `[ToString]` custom attribute.
* Removes the `[NotToString]` custom attributes.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithComplexCodeFix.cs name="ToString aspect with complex code fix"]

## Performance considerations

* Code fixes and refactorings are only useful at design time. At compile time, all code fixes will be ignored. To avoid generating code fixes at compile time, you can make your logic conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixTitles` expression.

* The `Func<ICodeActionBuilder, Task>`  delegate is only executed when the user selects the code fix or refactoring. However, the entire aspect will be executed again, which has two implications:
  * The logic that _creates_ the delegate must be highly efficient because it is rarely used. Any expensive logic should be moved to the _implementation_ of the delegate itself.
  * To avoid generating the delegate, you can make it conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixImplementations` expression.

* At design time, all code fix titles, including those added by the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method, are cached for the complete solution. Therefore, you should avoid adding a large number of suggestions. The current Metalama design is not suited for this scenario.


> [!div class="see-also"]
> <xref:video-code-fixes>
> <xref:live-template>


