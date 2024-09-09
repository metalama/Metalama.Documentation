---
uid: diagnostics
level: 300
summary: "The document provides a guide on how to report or suppress diagnostics, including errors, warnings, or info messages, from an aspect in C# programming. It also outlines the benefits and provides examples."
keywords: "diagnostics, errors, warnings, suppress, C# compiler, report diagnostics, suppress diagnostics, Metalama.Framework.Diagnostics, DiagnosticDefinition, SuppressionDefinition"
created-date: 2023-01-26
modified-date: 2024-08-04
---

# Reporting and suppressing diagnostics

This article provides guidance on how to report a diagnostic, including errors, warnings, or information messages, from an aspect or suppress a diagnostic reported by the C# compiler or another aspect.

## Benefits

* **Prevent non-intuitive compilation errors**: Aspects applied to unexpected or untested kinds of declarations can lead to confusing exceptions or errors during the compilation of the transformed code. This confusion can be mitigated by reporting clear error messages when the target of the aspect fails to meet expectations. Refer to <xref:eligibility> for this use case.
* **Eliminate confusing warnings**: The C# compiler and other analyzers, unaware of the code transformation by your aspect, may report irrelevant warnings. Suppressing these warnings with your aspect can reduce confusion and save developers from manually suppressing these warnings.
* **Enhance user productivity**: Overall, reporting and suppressing relevant diagnostics can significantly improve the productivity of those using your aspect.
* **Diagnostic-only aspects**: You can create aspects that solely report or suppress diagnostics without transforming any source code. Refer to <xref:validation> for additional details and benefits.

## Reporting a diagnostic

To report a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> specifies the diagnostic id, the severity, and the message formatting string.

    * For a message without formatting parameters or with weakly-typed formatting parameters, utilize the non-generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> class.
    * For a message with a single strongly-typed formatting parameter, employ the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition`1> class, e.g., `DiagnosticDefinition<int>`.
    * For a message with several strongly-typed formatting parameters, apply the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition`1> with a tuple, e.g., `DiagnosticDefinition<(int,string)>` for a message with two formatting parameters expecting a value of type `int` and `string`.

    > [!WARNING]
    > The aspect framework relies on diagnostics being defined as static fields of aspect classes. You will not be able to report a diagnostic that has not been declared on an aspect class of the current project.

3. To report a diagnostic, use the <xref:Metalama.Framework.Diagnostics.ScopedDiagnosticSink.Report*?text=builder.Diagnostics.Report> method.

    The second parameter of the `Report` method is optional: it specifies the declaration to which the diagnostic relates. Based on this declaration, the aspect framework computes the diagnostic file, line, and column. If you don't provide a value for this parameter, the diagnostic will be reported for the target declaration of the aspect.

### Example

The following aspect requires a field named `_logger` to exist in the target type. Its `BuildAspect` method checks the existence of this field and reports an error if it is absent.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ReportError.cs name="Report Error"]

## Suppressing a diagnostic

The C# compiler or other analyzers may report warnings to the target code of your aspects. Since neither the C# compiler nor the analyzers are aware of your aspect, some of these warnings may be irrelevant. As an aspect author, it is good practice to prevent the reporting of irrelevant warnings.

To suppress a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> specifies the identifier of the diagnostic to suppress.

3. Call the <xref:Metalama.Framework.Diagnostics.ScopedDiagnosticSink.Suppress*> method using `builder.Diagnostics.Suppress(...)` in the `BuildAspect` method and supply the <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> created above. The suppression will apply to the current target of the aspect unless you specify a different scope as an argument.

These steps will suppress _all_ warnings of the specified ID in the scope of the current target of the aspect. If you want to filter the warnings by text or argument, use the <xref:Metalama.Framework.Diagnostics.SuppressionDefinition.WithFilter*?text=SuppressionDefinition.WithFilter> method.



### Example

The following logging aspect requires a `_logger` field. This field will be used in generated code but never in user code. Because the IDE does not see the generated code, it will report the `CS0169` warning, which is misleading and annoying to the user. The aspect suppresses this warning.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SuppressWarning.cs name="Suppress Warning"]

## Advanced example

The following aspect can be added to a field or property. It overrides the getter implementation to retrieve the value from the service locator. This aspect assumes that the target class has a field named `_serviceProvider` and of type `IServiceProvider`. The aspect reports errors if this field is absent or does not match the expected type. The C# compiler may report a warning `CS0169` because it appears from the source code that the `_serviceProvider` field is unused. Therefore, the aspect must suppress this diagnostic.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/LocalImport.cs name="Import Service"]

## Validating the target code after all aspects have been applied

When your aspect's `BuildAspect` method is executed, it views the code model as it was _before_ the aspect was applied.

If you need to validate the code after all aspects have been applied, see <xref:aspect-validating>.





