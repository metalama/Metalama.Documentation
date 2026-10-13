---
uid: Metalama.Framework.Fabrics
summary: *content
created-date: 2023-01-26
modified-date: 2023-07-11
---

This namespace enables the addition of fabrics to your code. Similar to aspects, fabrics are executed at compile time. However, unlike aspects, fabrics do not require a custom attribute for integration into your code. The mere existence of the fabric in your code applies it to your code.

There are four types of fabrics, each applying to a different scope. All types of fabrics can add aspects and validators within their respective scopes. Type fabrics have the additional capability of adding advice to their scope, and project fabrics can set configuration options.

| Kind | Base Class | Scope | Abilities |
|-------|-|---------|--|
| Type Fabrics | <xref:Metalama.Framework.Fabrics.TypeFabric> | The containing type (type fabrics are nested types) and any member. | Add aspects, advice, and validators.
| Namespace Fabrics | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Any type in the namespace that contains the fabric type. | Add aspects and validators.
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Any type in the project that contains the fabric type or in any project. | Add aspects and validators, and set configuration options. Project fabrics can be inherited from parent directories.
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> | Any type in any project _referencing_ the containing project. | Add aspects and validators, and set configuration options.

> [!NOTE]
> For enhanced design-time performance and usability, it is strongly recommended to implement type fabrics in a separate file and mark the parent class as `partial`.

## Class diagrams

### Fabrics

```mermaid
classDiagram

    class Fabric {

    }

    class ProjectFabric {
        AmendProject(IProjectAmender)
    }

    class TransitiveProjectFabric {
    }

    Fabric <|-- ProjectFabric : derives from
    ProjectFabric <|-- TransitiveProjectFabric : derives from

    class NamespaceFabric {
        AmendNamespace(NamespaceAmender)
    }

    Fabric <|-- NamespaceFabric : derives from

    class TypeFabric {
        AmendType(TypeAmender)
    }

    Fabric <|-- TypeFabric : derives from


```

### Amenders

```mermaid
classDiagram

    class IAmender {
        Project
    }

    class IDeclarationSelector {
        With()
    }

    IDeclarationSelector <|-- IAmender : derives from

    class IDeclarationSelection {
        AddAspect()
        AddAnnotation()
        RegisterValidator()
        RequireAspect()
    }

    IDeclarationSelection <-- IDeclarationSelector : creates

    class IProjectAmender {

    }

    IAmender <|-- IProjectAmender
    IAmender <|-- INamespaceAmender
    IAmender <|-- ITypeAmender

    class INamespaceAmender  {
        Namespace
    }

    class ITypeAmender  {
        Type
        Advice : IAdviceFactory
    }

    IAdviceFactory <-- ITypeAmender : exposes

```

## Namespace member

