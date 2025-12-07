---
uid: code-fixes
level: 300
summary: "Instructions on offering code fixes and refactorings using the Metalama Framework, including attaching code fixes to diagnostics, suggesting refactorings without diagnostics, and building multi-step code fixes. Also discusses performance considerations."
keywords: "code fixes, refactorings, Metalama Framework, diagnostics, code fix suggestions, multi-step code fixes, CodeFixFactory, ScopedDiagnosticSink, ICodeActionBuilder, performance considerations"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Offering code fixes and refactorings

## Attaching code fixes to diagnostics

To attach a code fix to a diagnostic:

1. Add the `Metalama.Extensions.CodeFixes` package.
2. After creating the diagnostic, call the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> extension method.
3. Use the <xref:Metalama.Extensions.CodeFixes.CodeFixFactory> class to create predefined, single-step code fixes, such as adding or removing a custom attribute. For more complex code fixes, see <xref:code-fixes#building-custom-code-fixes>.

## Suggesting refactorings without diagnostics

Aspects and fabrics can suggest code refactorings without reporting diagnostics by calling the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method.

### Example: code fix without diagnostic

The following example demonstrates an aspect that implements the `ToString` method. By default, it includes all public properties of the class in the `ToString` result. However, developers can opt out by adding `[NotToString]` to any property.

The aspect uses the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method to add a code fix suggestion for all properties not yet annotated with `[NotToString]`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithSimpleCodeFix.cs name="ToString aspect with simple code fix"]

## Building custom code fixes

To create a custom code fix, instantiate the <xref:Metalama.Extensions.CodeFixes.CodeFix> class using the constructor instead of the <xref:Metalama.Extensions.CodeFixes.CodeFixFactory> class.

The <xref:Metalama.Extensions.CodeFixes.CodeFix> constructor accepts two arguments:

* The _title_ of the code fix, which is displayed to the user
* A _delegate_ of type `Func<ICodeActionBuilder, Task>` that applies the code fix when the user selects it

The title must be globally unique for the target declaration. Even two different aspects can't provide two code fixes with the same title to the same declaration.

The delegate typically uses one of the following methods of the <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder> interface:

| Method | Description |
|------|----|
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.AddAttributeAsync*> | Adds a custom attribute to a declaration.
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.RemoveAttributesAsync*> | Removes all custom attributes of a given type from a given declaration and all contained declarations.
| <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> | Transforms the source code using an aspect (as if it were applied as a live template).

### Example: custom code fix

The previous example continues here, but instead of a single-step code fix, we offer users the ability to switch from an aspect-oriented implementation of `ToString` by applying the aspect to the source code itself.

The custom code fix performs the following actions:

* Applies the aspect using the <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> method.
* Removes the `[ToString]` custom attribute.
* Removes the `[NotToString]` custom attributes.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithComplexCodeFix.cs name="ToString aspect with complex code fix"]

## Performance considerations

* Code fixes and refactorings are only useful at design time. At compile time, all code fixes are ignored. To avoid generating code fixes at compile time, make your logic conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixTitles` expression.

* The `Func<ICodeActionBuilder, Task>` delegate is only executed when you select the code fix or refactoring. However, the entire aspect is executed again, which has two implications:
  * The logic that _creates_ the delegate must be highly efficient because it's rarely used. Move any expensive logic to the _implementation_ of the delegate itself.
  * To avoid generating the delegate, make it conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixImplementations` expression.

* At design time, all code fix titles, including those added by the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*> method, are cached for the complete solution. Therefore, avoid adding a large number of suggestions. The current Metalama design isn't suited for this scenario.

> [!div class="see-also"]
> <xref:live-template>
> <xref:building-ide-interactions>
> <xref:diagnostics>
> <xref:Metalama.Extensions.CodeFixes.CodeFix>
> <xref:Metalama.Extensions.CodeFixes.CodeFixFactory>
> <xref:Metalama.Extensions.CodeFixes.ICodeActionBuilder>
> <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions>
