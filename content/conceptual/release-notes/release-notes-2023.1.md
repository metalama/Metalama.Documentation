---
uid: release-notes-2023.1
summary: "The Metalama 2023.1 update focuses on bug fixes and minor enhancements, including improved support for Visual Studio 17.6, Roslyn 4.6, and .NET SDK 7.0.300, and better error reporting."
keywords: "Metalama 2023.1, release notes"
created-date: 2023-12-11
modified-date: 2024-08-04
---

# Metalama 2023.1

Metalama 2023.1 focuses on bug fixes and minor enhancements.

## Platform updates

* Visual Studio 17.6
* Roslyn 4.6
* .NET SDK 7.0.300

## Enhancements

* Support for Visual Studio 17.6, Roslyn 4.6, and .NET SDK 7.0.300.
* The <xref:Metalama.Framework.Aspects.AdviserExtensions.ImplementInterface*?text=AdviserExtensions.ImplementInterface> method now exposes the created members under the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.InterfaceMembers?IImplementInterfaceAdviceResult.InterfaceMembers> property.
* New extension method <xref:Metalama.Framework.Code.TypeExtensions.ToTypeOfExpression*?text=IType.ToTypeOfExpression>, which returns an <xref:Metalama.Framework.Code.IExpression>.
* Contracts now support `IEnumerable`.
* Ability to make an introduced field `readonly`.
* Improved error reporting for aspect members with more than one advice/template attribute.
* Enhanced error message when referencing a non-existing type in compile-time code.
* In templates, `foreach` loops are now allowed in run-time-conditional blocks.
* Better error reporting when the `[Template]` attribute is used on accessors.

> [!div class="see-also"]
> <xref:release-notes>
