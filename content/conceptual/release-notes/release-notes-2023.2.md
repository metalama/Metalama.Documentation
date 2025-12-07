---
uid: release-notes-2023.2
summary: "The Metalama 2023.2 update includes bug fixes, minor enhancements, platform updates for compatibility with xUnit 2.5.0, and new APIs to filter collections by unbound generic types."
keywords: "Metalama 2023.2, release notes"
created-date: 2023-12-11
modified-date: 2024-08-04
---

# Metalama 2023.2

Metalama 2023.2 focuses on bugfixes and minor enhancements.

## Platform updates

- Our test framework is now compatible with xUnit 2.5.0.

## Enhancements

- The <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> annotation can now be added to type fabrics, which prompts the type fabric invocation not only for the target type but also for all derived types.
- In the code model, we've introduced APIs to filter collections by unbound generic types:
  - <xref:Metalama.Framework.Code.ConversionKind.TypeDefinition?text=ConversionKind.TypeDefinition> has been added to match type definitions while ignoring any generic argument.
  - New methods on the <xref:Metalama.Framework.Code.Collections.IAttributeCollection> interface: <xref:Metalama.Framework.Code.Collections.IAttributeCollection.OfAttributeType*>, <xref:Metalama.Framework.Code.Collections.IAttributeCollection.Any*>.
  - New method on the <xref:Metalama.Framework.Code.Collections.INamedTypeCollection> interface: <xref:Metalama.Framework.Code.Collections.INamedTypeCollection.OfTypeDefinition*>.

## Breaking changes

- The interpretation of <xref:Metalama.Framework.Code.ConversionKind.Default?text=ConversionKind.Default>, utilized as a default parameter value in the <xref:Metalama.Framework.Code.TypeExtensions.Is*> API, has been adjusted to more accurately reflect the documented behavior of the C# `is` operator. This impacts any method accepting <xref:Metalama.Framework.Code.ConversionKind>, whether explicitly specified or not.
