---
uid: release-notes-2025.1
summary: "Metalama 2025.1 is the first open-source release with breaking changes in packages and namespaces for proprietary components."
keywords: "Metalama 2025.1, release notes"
created-date: 2025-05-01
modified-date: 2025-05-01
---

# Metalama 2025.1

Metalama 2025.1 is the first open-source version of Metalama. Some components remain proprietary and commercial. Completing this release required splitting some packages and namespaces, resulting in breaking changes.

You might need to add new packages to your project and perform a find-and-replace-all operation to address the renamed namespaces.

## Breaking changes

* The namespace `Metalama.Extensions.CodeFixes` has been renamed `Metalama.Extensions.CodeFixes` and moved to a package of the same name.
* The namespace `Metalama.Extensions.Validation` has been renamed `Metalama.Extensions.Validation` and moved to a package of the same name.
* The interfaces `IAspectReceiver` and `IValidatorReceiver` have been refactored into a single interface and more abstract <xref:Metalama.Framework.Fabrics.IQuery> interface with several extension methods in <xref:Metalama.Framework.Fabrics.QueryExtensions>, <xref:Metalama.Framework.Aspects.AspectQueryExtensions> or <xref:Metalama.Extensions.Validation.ValidationQueryExtensions>. Refactoring interface methods into extension methods has some impact:

  * The namespace of extension methods must be explicitly imported through `using`.
  * Generic arguments, when provided explicitly, must be rewritten.

* The <xref:Metalama.Extensions.Validation.ValidationQueryExtensions.AfterAllAspects*> and <xref:Metalama.Extensions.Validation.ValidationQueryExtensions.BeforeAnyAspect*> methods are obsolete. They're replaced by an `options` parameter on the <xref:Metalama.Extensions.Validation.ValidationQueryExtensions.Validate*> method.

* The already obsolete `Metalama.Extensions.Architecture.Fabrics` namespace has been removed.
* _Metalama Free_ licenses are not supported in Metalama 2025.1. If you want to continue using proprietary features, you'll have to choose another license.
* The <xref:Metalama.Framework.Code.ReferenceKinds> type has moved to the `Metalama.Framework.Code` namespace.
* The `IClassIntroductionAdviceResult` interface has been removed and replaced by its ancestor <xref:Metalama.Framework.Advising.IIntroductionAdviceResult`1>.

> [!div class="see-also"]
> <xref:release-notes>
