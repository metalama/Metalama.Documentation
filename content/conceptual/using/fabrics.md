---
uid: fabrics
level: 200
summary: "The document discusses fabrics in the Metalama framework, which are unique classes that execute at compile time. They can add aspects, configure libraries, and implement architecture rules."
keywords: "fabrics, Metalama framework, configure libraries, architecture rules, ProjectFabric, Namespace Fabric, Type Fabric, Transitive Project Fabrics"
created-date: 2024-08-04
modified-date: 2025-12-07
---

# Fabrics

The previous article discussed how to add multiple aspects at once using compile-time imperative code instead of declarative custom attributes. It introduced a single type of fabric: <xref:Metalama.Framework.Fabrics.ProjectFabric>. However, there are several other types of fabrics and many use cases for them.

Even if you don't plan to create your own aspects, understanding fabrics enhances your proficiency with Metalama.

_Fabrics_ are special classes in your code that execute at compile time within the compiler and at design time within your IDE. Unlike aspects, fabrics don't need to be _applied_ to any declaration or _called_ from anywhere. Their primary method is invoked at the appropriate time simply because it exists in your code. Think of fabrics as _compile-time entry points_.

With fabrics, you can:

- Add aspects programmatically using LINQ-like code queries, instead of marking individual declarations with custom attributes. See <xref:fabrics-adding-aspects>.
- Configure aspect libraries. See <xref:fabrics-configuration>.
- Implement architecture rules in your code. See <xref:validation>.

In addition to <xref:Metalama.Framework.Fabrics.ProjectFabric>, there are three more types of fabric:

| Fabric Type | Abstract Class | Purpose |
|-------------|----------------|---------|
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in the _current_ project. |
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in projects that _reference_ the current project. |
| Namespace Fabric | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Add aspects or architecture rules to the namespace that contains the fabric type. |
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | Add aspects to different members of the type that contains the nested fabric type. |

## Abilities of fabrics

### 1. Adding aspects programmatically

All fabric types can add aspects to declarations using LINQ-like queries. This is the most common use case for fabrics.

For details, see <xref:fabrics-adding-aspects>.

### 2. Configuring aspect libraries

All fabric types can set options that configure how aspect libraries behave. This lets you customize logging formats, caching policies, and other aspect-specific settings. Configuration scope depends on the fabric type.

For details, see <xref:fabrics-configuration>.

### 3. Reporting and suppressing diagnostics

Fabrics can report custom diagnostics (errors, warnings, or information messages) and suppress diagnostics reported by the compiler or other analyzers.

For details, see <xref:diagnostics>.

### 4. Validating architecture

Project fabrics, transitive project fabrics, and namespace fabrics can register architecture validators that enforce coding standards and architectural rules across your codebase.

For details, see <xref:validation>.

### 5. Adding advice to a type (Type Fabric only)

Type fabrics have a unique ability: they can directly add advice (such as method overrides or member introductions) to their containing type without requiring a separate aspect. This makes type fabrics function like embedded aspects.

For details, see <xref:fabrics-advising>.

## Fabrics vs aspects

Aspects and fabrics serve different purposes:

- **Aspects** are reusable APIs that must be applied to target declarations. They encapsulate behavior that can be applied to many declarations, shared across projects, and distributed as NuGet packages. Aspects are the building blocks of aspect-oriented programming.

- **Fabrics** are compile-time entry points automatically called by the framework. They don't need to be applied to anything. Fabrics are consumers, not APIs: they're project-specific entry points that use aspects and configure them. Fabrics aren't reusable across projects in the same way aspects are.

To create reusable logic for fabrics, define extension methods operating on <xref:Metalama.Framework.Fabrics.IAmender`1> or one of its derived interfaces. These extension methods can then be called from any fabric.

| Use Case | Recommended Approach |
|----------|---------------------|
| Apply behavior to specific declarations marked with an attribute | Use an <xref:aspects?text=aspect> as a custom attribute |
| Apply behavior to many declarations based on a pattern | Use a fabric to <xref:fabrics-adding-aspects?text=add aspects programmatically> |
| Create reusable transformation logic | Create an <xref:aspects?text=aspect> |
| Create reusable logic for adding aspects, configuring, or validating | Create an extension method on <xref:Metalama.Framework.Fabrics.IAmender`1> |
| Configure aspect library settings for a project | Use a <xref:fabrics-configuration?text=project fabric> |
| Enforce architecture rules across a project | Use a <xref:validation?text=project or namespace fabric> |
| Apply policies to all projects referencing a library | Use a <xref:fabrics-many-projects?text=transitive project fabric> |
| Add advice to a single type without creating a reusable aspect | Use a <xref:fabrics-advising?text=type fabric> |

> [!div class="see-also"]
>
> **See also**
>
> - <xref:using-metalama>
> - <xref:fabrics-adding-aspects>
> - <xref:fabrics-configuration>
> - <xref:fabrics-many-projects>
> - <xref:validation>
