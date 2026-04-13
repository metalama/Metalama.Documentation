---
uid: fabrics-advising
summary: "The document provides a guide on how to advise a type using a type fabric, a compile-time nested class, in the Metalama Framework. It includes a step-by-step process and examples."
level: 300
keywords: "type fabric, Metalama Framework, compile-time nested class, type-level aspect, AmendType method, ITypeAmender, IAdviser, advanced aspects, member introductions, helper aspect, target type, ScopedDiagnostics"
created-date: 2023-01-26
modified-date: 2026-04-13
---

# Advising a single type with a fabric

Instead of using aspects, you can advise the current type using a type fabric. A type fabric is a compile-time nested class that functions as a type-level aspect added to the target type.

## Setting up a type fabric

To advise a type using a type fabric, follow these steps:

1. Create a nested type derived from the <xref:Metalama.Framework.Fabrics.TypeFabric> class.

    > [!NOTE]
    > For optimal design-time performance and usability, we recommend implementing type fabrics in a separate file and marking the containing type as `partial`.

2. Override the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

3. Call advising methods directly on the `amender` parameter. <xref:Metalama.Framework.Fabrics.ITypeAmender> implements <xref:Metalama.Framework.Aspects.IAdviser`1>, so you can use all extension methods from <xref:Metalama.Framework.Aspects.AdviserExtensions> directly, such as `IntroduceMethod`, `Override`, or `ImplementInterface`. To use this feature, you must be familiar with advanced aspects. For more details, refer to <xref:advising-code>.

    To advise a member of the target type instead of the type itself, use the <xref:Metalama.Framework.Aspects.IAdviser.With*> method to obtain an adviser for that member.

4. Optionally, you can add declarative advice, such as member introductions, to your type fabrics. For more information, see <xref:introducing-members>.

> [!NOTE]
> Type fabrics are always executed first, before any aspect. As a result, they can only add advice to members defined in the source code. If you need to add advice to members introduced by an aspect, you'll need to use a helper aspect and order it _after_ the aspects that provide the members you wish to advise.

## Reporting diagnostics

The <xref:Metalama.Framework.Fabrics.ITypeAmender> exposes a <xref:Metalama.Framework.Fabrics.ITypeAmender.Diagnostics> property of type <xref:Metalama.Framework.Diagnostics.ScopedDiagnosticSink>. You can use it to report or suppress diagnostics scoped to the target type, just as you would in an aspect's `BuildAspect` method.

For details on how to define and report diagnostics, see <xref:diagnostics>.

## Example: introducing members

The following example demonstrates how to create a type fabric that introduces ten methods to the target type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabric.cs name="Type Fabric Adding Advice"]

## Example: overriding a method and reporting a diagnostic

The following example shows how to use a type fabric to override a method and report a diagnostic when the target type is missing the expected `Name` property. Notice how advice extension methods are called directly on the `amender` and the <xref:Metalama.Framework.Aspects.IAdviser.With*> method is used to advise a specific method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabricDiagnostics.cs name="Type Fabric With Diagnostics"]

> [!div class="see-also"]
> <xref:fabrics>
> <xref:advising-code>
> <xref:introducing-members>
> <xref:ordering-aspects>
> <xref:diagnostics>
> <xref:Metalama.Framework.Fabrics.TypeFabric>
> <xref:Metalama.Framework.Fabrics.ITypeAmender>
> <xref:Metalama.Framework.Aspects.IAdviser`1>
> <xref:Metalama.Framework.Aspects.AdviserExtensions>
