---
uid: aspect-validating
level: 300
summary: "The document explains how to validate code from an aspect using Metalama Framework, covering validation before or after aspects and validating code references. It includes examples and steps to follow."
keywords: "validating code, Metalama Framework, aspect validation, code references, .NET aspects, IAspect BuildAspect, diagnostic reporting, DeclarationValidationContext, ReferenceValidationContext, validator granularity"
created-date: 2023-03-20
modified-date: 2024-08-04
---

# Validating code from an aspect

> [!NOTE]
> This feature requires a Metalama Professional license.

Validating source code and providing meaningful error messages is a critical feature of most aspects. Failure to do so can result in confusing error messages for the aspect's user or even invalid behavior at runtime.

The first two techniques for validating code involve defining eligibility (see <xref:eligibility>) and reporting errors from the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method (see <xref:diagnostics>). In this article, we introduce two additional techniques:

* Validating the code _before_ applying any aspect, or _after_ applying all aspects.
* Validating _references_ to the target declaration of the aspect.

## Validating code before or after aspects

By default, the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> receives the version of the code model _before_ applying the current aspect. However, there may be instances where you need to validate a different version of the code model. Metalama allows you to validate three versions:

* Before the current aspect has been applied,
* _Before any_ aspect has been applied, or
* _After all_ aspects have been applied.

To validate a different version of the code model, follow these steps:

1. Define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method with the signature `void Validate(in DeclarationValidationContext context)`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Extensions.Validation.DeclarationValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>.
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect. From this method:
   1. Access the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound*?text=builder.Outbound> property,
   2. Select declarations to be validated using the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> and <xref:Metalama.Framework.Fabrics.IQuery`1.Select*> methods,
   2. Call the <xref:Metalama.Extensions.Validation.ValidationQueryExtensions.Validate*> method, pass a delegate to the validation method, and pass a <xref:Metalama.Extensions.Validation.DeclarationValidationOptions>, setting its <xref:Metalama.Extensions.Validation.DeclarationValidationOptions.Time> property to <xref:Metalama.Extensions.Validation.DeclarationValidationTime.BeforeAnyAspect> or <xref:Metalama.Extensions.Validation.DeclarationValidationTime.AfterAllAspects>.

### Example: requiring a later aspect to be applied

The following example demonstrates how to validate that the target type of the `Log` aspect contains a field named `_logger`. The implementation allows the `_logger` field to be introduced _after_ the `Log` aspect has been applied, thanks to the <xref:Metalama.Extensions.Validation.DeclarationValidationOptions> parameter.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ValidateAfterAllAspects.cs]

## Validating code references

Aspects can validate not only the declaration to which they are applied but also how this target declaration is used. In other words, aspects can validate _code references_.

In order to optimize performance, Metalama tries to avoid validating every single code reference. Instead, it has a concept of _validator granularity_ (<xref:Metalama.Extensions.Validation.ReferenceGranularity>), which accepts the values `Compilation`, `Namespace`, `Type`, `Member`, or `ParameterOrAttribute`. When a validator is invariant within some level of granularity, then its predicate should only be evaluated once within the declaration at this level of granularity. For instance, if a validator granularity is set to `Namespace`, then _all_ references within that namespace will be either valid or invalid at the same time. Therefore, Metalama will only make a single call for this whole namespace. The validator method will be prevented from accessing details of a finer granularity level than the declared one. Warnings will be reported on all code references.

To create an aspect that validates references:

1. In the aspect class, define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method of arbitrary name with the signature `void ValidateReference( ReferenceValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Extensions.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>. Alternatively, you can create a class implementing the <xref:Metalama.Extensions.Validation.InboundReferenceValidator> abstract class.
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect. From this method:
   1. Access the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound*?text=builder.Outbound> property,
   2. Select declarations to be validated using the <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> and <xref:Metalama.Framework.Fabrics.IQuery`1.Select*> methods,
   3. Call the <xref:Metalama.Extensions.Validation.ReferenceValidationQueryExtensions.ValidateInboundReferences*> method. Pass a delegate to the validation method or an instance of the validator class and the <xref:Metalama.Extensions.Validation.ReferenceGranularity>.

> [!NOTE]
> The delegate passed to the <xref:Metalama.Extensions.Validation.ReferenceValidationQueryExtensions.ValidateInboundReferences*> method must point to a named method of the aspect.

### Example: ForTestOnly, aspect implementation

The following example implements a custom attribute `[ForTestOnly]` that enforces that the target of this attribute can only be used from a namespace that ends with `.Tests.`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ForTestOnly.cs name="For Test Only, Aspect Implementation"]


