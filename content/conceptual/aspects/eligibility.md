---
uid: eligibility
level: 300
summary: "The document explains how to define eligibility for aspects in code, ensuring predictable behavior, standard error messages, and relevant IDE suggestions. It provides examples and discusses benefits, custom eligibility conditions, eligibility builders, and when to emit custom errors."
keywords: "eligibility, aspect, declarations, build errors, correct run-time behavior, Metalama.Framework.Eligibility, eligibility conditions, IEligibilityBuilder, MustSatisfy, custom errors"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Defining the eligibility of aspects

Most aspects are designed and implemented for specific kinds of target declarations. For instance, you may decide that your caching aspect won't support `void` methods or methods with `out` or `ref` parameters. As the aspect author, ensure that users of your aspect apply it only to the declarations you expect. Otherwise, the aspect may cause build errors with confusing messages or even incorrect run-time behavior.

## Benefits

Defining the eligibility of an aspect provides the following benefits:

* **Predictable behavior**: Applying an aspect to a declaration for which the aspect wasn't designed or tested can be confusing for your users due to error messages they may not understand. As the aspect author, ensure that using your aspect is easy and predictable.
* **Standard error messages**: All eligibility error messages are standard, making them easier to understand for aspect users.
* **Relevant suggestions in the IDE**: The IDE will only propose code actions in the refactoring menu for eligible declarations.

## Defining eligibility

To define the eligibility of your aspect, implement or override the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method of the aspect. Use the `builder` parameter, which is of type <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1>, to specify the requirements of your aspect. For instance, use <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustNotBeAbstract*?text=builder.MustNotBeAbstract()> to require a non-abstract method.

> [!NOTE]
> Your implementation of <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> must not reference any instance member of the class. This method is called on an instance obtained using `FormatterServices.GetUninitializedObject`, i.e., _without invoking the class constructor_.

### Example: allowing instance methods only

In the following example, we restrict the eligibility of a logging aspect to non-static methods.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Eligibility.cs name="Eligibility"]

## Validating the declaring type, parameter type, or return type

The `Must*` methods of the <xref:Metalama.Framework.Eligibility.EligibilityExtensions> class apply to the direct target of the aspect. If you want to validate something else, such as the declaring type of the member or the method return type, use methods like <xref:Metalama.Framework.Eligibility.EligibilityExtensions.DeclaringType*>, <xref:Metalama.Framework.Eligibility.EligibilityExtensions.ReturnType*>, or <xref:Metalama.Framework.Eligibility.EligibilityExtensions.Parameter*> before calling the `Must*` method.

The benefit of using these methods is that the error message is more informative when the user attempts to add the aspect to an ineligible condition.

### Example: allowing static types only

In the following example, we require the aspect to be used with static types only.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Eligibility_DeclaringType.cs name="Eligibility"]

Notice how informative the error message in the target code is: the use of <xref:Metalama.Framework.Eligibility.EligibilityExtensions.DeclaringType*> informs Metalama to use this information in the error message for the user's benefit.

## Defining custom eligibility conditions

The <xref:Metalama.Framework.Eligibility.EligibilityExtensions> class defines the most common eligibility conditions. However, you'll often need to express conditions for which no ready-made method exists. In this situation, you can add a custom eligibility condition by calling <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*> and define your condition using the <xref:Metalama.Framework.Code> namespace. You must supply two lambda expressions:

1. The first lambda is a predicate that should return `true` if the proposed declaration is a valid target.
2. The second lambda is only evaluated when the proposed declaration is _not_ a valid target and should return a user-readable string that explains why the declaration isn't eligible.

    * This lambda must return a _formattable_ string. Formatting the string yourself isn't recommended as we're using a custom formatter.
    * To include the description of the ineligible declaration in the formattable string, just use the raw input argument. It will be properly formatted.
    * We adopted the convention that this message says what the declaration _must_ be to be eligible instead of saying what it _must not_ be because it generally combines better when there are several eligibility conditions.

    For instance, if your aspect doesn't support `record` types, use `t => $"{t} must not be a record type"`.

### Example: forbidding record types

The following example demonstrates the use of <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*> to mark record types as ineligible.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Eligibility_Custom.cs name="Eligibility"]

## Adding "if" clauses to eligibility

Sometimes the eligibility of aspects depends on a condition. For instance, your aspect may be eligible for all instance methods but only `void` static methods. One approach is to use <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*> to create a custom condition. A more straightforward approach is to use the <xref:Metalama.Framework.Eligibility.EligibilityExtensions.If*> method.

## Converting eligibility builders

To convert an <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1> of one declaration type to an <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1> for another type, use the `builder.Convert().To<T>()` method. This will implicitly add an eligibility condition that the declaration is a `T`.

Alternatively, when you don't want this implicit condition, you can use `builder.Convert().When<T>()`. This is equivalent to using an <xref:Metalama.Framework.Eligibility.EligibilityExtensions.If*> followed by a `Convert().To<T>()`.

## When to emit custom errors instead

It may be tempting to add an eligibility condition for every requirement of your aspect instead of emitting a custom error message. However, this may be confusing for the user.

As a rule of thumb, use eligibility to define those declarations for which it makes sense to apply the aspect or not and use error messages when the aspect makes sense on the declaration, but some contingency may prevent the aspect from being used. This is where you should report errors.

For instance:

* Adding a caching aspect to a `void` method doesn't make sense and should be addressed with eligibility. However, the fact that your aspect doesn't support methods returning a collection is a limitation caused by your particular implementation and should be reported using a custom error.

* Adding a dependency injection aspect to an `int` or `string` field doesn't make sense, and this condition should be expressed using the eligibility API. However, the fact that your implementation of the aspect requires the field to be non-read-only is a contingency and should be reported as an error.

For details about reporting errors, see <xref:diagnostics>.

### Example: combining eligibility with diagnostics

The following example expands the previous one, reporting custom errors when the target class doesn't define a field `logger` of type `TextWriter`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/EligibilityAndValidation.cs name="Eligibility and Validation"]

> [!div class="see-also"]
> <xref:diagnostics>
> <xref:aspect-inheritance>
> <xref:Metalama.Framework.Eligibility.IEligible`1>
> <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1>
> <xref:Metalama.Framework.Eligibility.EligibilityExtensions>
