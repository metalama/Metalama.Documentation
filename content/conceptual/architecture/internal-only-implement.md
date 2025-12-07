---
uid: internal-only-implement
level: 200
summary: "Use Metalama's InternalOnlyImplementAttribute to restrict interface implementation and prevent other assemblies from implementing your interface."
keywords: "restrict interface implementation, prevent others from implementing, Metalama, InternalOnlyImplementAttribute, interface protection, .NET, add Metalama.Extensions.Architecture, attribute, prevent other assemblies, warning"
created-date: 2023-03-22
modified-date: 2025-11-30
---

# Restricting who can implement an interface

When designing an interface, it's sometimes preferable to restrict its implementation to prevent others from implementing it. This is because once an interface is implemented, adding new members is no longer possible without breaking any class that implements it.

Metalama offers a solution to protect your interface from being implemented by other assemblies. Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Annotate the interface with the <xref:Metalama.Extensions.Architecture.Aspects.InternalOnlyImplementAttribute> attribute. This attribute prevents any other assemblies from implementing your interface.

## Example

In the parent project, let's assume we have the following interface protected by the <xref:Metalama.Extensions.Architecture.Aspects.InternalOnlyImplementAttribute> attribute:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/InternalOnlyImplement.Dependency.cs]

If we attempt to implement this interface in a child project, a warning is reported:

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/InternalOnlyImplement.cs tabs="target"]

> [!div class="see-also"]
> <xref:validation>
> <xref:Metalama.Extensions.Architecture.Aspects.InternalOnlyImplementAttribute>
