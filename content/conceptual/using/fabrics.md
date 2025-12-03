---
uid: fabrics
level: 200
summary: "The document discusses fabrics in the Metalama framework, which are unique classes that execute at compile time. They can add aspects, configure libraries, and implement architecture rules."
keywords: "fabrics, Metalama framework, configure libraries, architecture rules, ProjectFabric, Namespace Fabric, Type Fabric, Transitive Project Fabrics"
created-date: 2024-08-04
modified-date: 2025-11-30
---

# Fabrics

In the previous article, we discussed how to add multiple aspects at once using compile-time imperative code, as opposed to declarative custom attributes. We introduced a single type of fabric: <xref:Metalama.Framework.Fabrics.ProjectFabric>. However, there are several other types of fabrics and numerous use cases for them.

Even if you currently have no plans to create your own aspects, understanding fabrics will enhance your proficiency with Metalama.

_Fabrics_ are unique classes in your code that execute at compile time within the compiler and at design time within your IDE. Unlike aspects, fabrics don't need to be _applied_ to any declaration or _called_ from anywhere. Their primary method will be invoked at the appropriate time simply because it exists in your code. Therefore, you can think of fabrics as _compile-time entry points_.

With fabrics, you can:

- Add aspects programmatically using LINQ-like code queries, instead of marking individual declarations with custom attributes. See <xref:fabrics-adding-aspects>.
- Configure aspect libraries. See <xref:fabrics-configuration>.
- Implement architecture rules to your code. See <xref:validation>.

In addition to <xref:Metalama.Framework.Fabrics.ProjectFabric>, there are three more types of fabric:

| Fabric Type | Abstract Class | Purpose |
|-------------|----------------|---------|
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in the _current_ project. |
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in projects that _reference_ the current project. |
| Namespace Fabric | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Add aspects or architecture rules to the namespace that contains the fabric type. |
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | Add aspects to different members of the type that contains the nested fabric type. |

Let's now explore the second use case of fabrics: configuration.

> [!div class="see-also"]
>
> **See also**
>
> <xref:using-metalama>
> <xref:fabrics-adding-aspects>
> <xref:fabrics-configuration>
> <xref:fabrics-many-projects>
> <xref:validation>
