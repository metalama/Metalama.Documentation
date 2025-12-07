---
uid: fabrics-advising
summary: "The document provides a guide on how to advise a type using a type fabric, a compile-time nested class, in the Metalama Framework. It includes a step-by-step process and an example."
level: 300
keywords: "type fabric, Metalama Framework, compile-time nested class, type-level aspect, AmendType method, IAspectBuilder.Advice, advanced aspects, member introductions, helper aspect, target type"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Advising a single type with a fabric

Instead of using aspects, you can advise the current type using a type fabric. A type fabric is a compile-time nested class that functions as a type-level aspect added to the target type.

To advise a type using a type fabric, follow these steps:

1. Create a nested type derived from the <xref:Metalama.Framework.Fabrics.TypeFabric> class.

    > [!NOTE]
    > For optimal design-time performance and usability, we recommend implementing type fabrics in a separate file and marking the containing type as `partial`.

2. Override the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

3. Use the methods exposed on the <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice?text=amender.Advice> property. To use this feature, you must be familiar with advanced aspects. For more details, refer to <xref:advising-code>.

4. Optionally, you can add declarative advice, such as member introductions, to your type fabrics. For more information, see <xref:introducing-members>.

> [!NOTE]
> Type fabrics are always executed first, before any aspect. As a result, they can only add advice to members defined in the source code. If you need to add advice to members introduced by an aspect, you'll need to use a helper aspect and order it _after_ the aspects that provide the members you wish to advise.

## Example

The following example demonstrates how to create a type fabric that introduces ten methods to the target type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabric.cs name="Type Fabric Adding Advice"]

> [!div class="see-also"]
> <xref:fabrics>
> <xref:advising-code>
> <xref:introducing-members>
> <xref:ordering-aspects>
> <xref:Metalama.Framework.Fabrics.TypeFabric>
> <xref:Metalama.Framework.Fabrics.ITypeAmender>
