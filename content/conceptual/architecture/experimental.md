---
uid: experimental
level: 200
summary: "Mark APIs as experimental using Metalama's ExperimentalAttribute attribute or Experimental compile-time method to warn users about potential future changes."
keywords: "experimental API, Metalama, ExperimentalAttribute, Obsolete attribute, warnings, Metalama.Extensions.Architecture"
created-date: 2023-03-23
modified-date: 2025-11-30
---

# Marking experimental APIs

The [[Obsolete]](xref:System.ObsoleteAttribute) attribute generates a warning when the marked declaration is used, unless the referencing declaration is also marked as `[Obsolete]`.

Sometimes you need a warning for an experimental API that may change or be removed later. The `[Obsolete]` attribute isn't ideal because its error message could mislead users. Instead, Metalama provides the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute> attribute and the <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.Experimental*> compile-time method for this purpose.

## Marking a specific API as experimental

To generate warnings when an experimental API is used, it's best to use the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute> attribute. Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Annotate the API with the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute>.

### Example: Using the Experimental attribute

In the following example, the `ExperimentalApi` class is explicitly marked as experimental.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Experimental.cs tabs="target"]

## Programmatically marking APIs as experimental

To mark several APIs as experimental using a programmatic rule instead of hand-picking declarations, use fabrics. Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Create or reuse a fabric type as described in <xref:fabrics>.

3. Import the <xref:Metalama.Framework.Fabrics> and <xref:Metalama.Extensions.Architecture> namespaces to benefit from extension methods.

4. Edit the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*>, or <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

5. Select the experimental APIs using the <xref:Metalama.Framework.Fabrics.IQuery`1.Select*>, <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*>, and <xref:Metalama.Framework.Fabrics.IQuery`1.Where*> methods.

6. Call the <xref:Metalama.Extensions.Architecture.ArchitectureExtensions.Experimental*> method.

### Example: Using the Experimental compile-time method

In the following example, all public members of `ExperimentalNamespace` are programmatically marked as experimental.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Experimental_Fabric.cs tabs="target"]

> [!div class="see-also"]
> <xref:validation>
> <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute>
> <xref:fabrics>
