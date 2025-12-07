---
uid: patterns
summary: "Metalama Patterns are libraries of design patterns for C#, developed by the Metalama team, and are available on GitHub under the MIT license."
level: 100
keywords: "design patterns, C#, Metalama Patterns"
created-date: 2024-06-11
modified-date: 2025-12-07
---

# Metalama Patterns

Metalama Patterns is a collection of aspect libraries that implement common design patterns for C#. You'll find these libraries under the <xref:patterns-api?text=Metalama.Patterns> namespace.

The Metalama team builds and maintains these patterns to the same quality standards as the Metalama framework itself.

> [!NOTE]
> Metalama Patterns are released under the open-source MIT license and are available on [GitHub](https://github.com/metalama/Metalama/tree/HEAD/Metalama.Patterns).

The following libraries are currently available:

| Library | Description |
|---------|-------------|
| <xref:dependency-injection-aspect> | Integrates dependency injection into aspects and provides an aspect that automatically pulls dependencies from fields and properties, with lazy loading support. |
| <xref:contract-patterns> | Implements contract-based programming through preconditions, postconditions, and invariants. |
| <xref:caching> | Caches method results with support for invalidation, dependencies, and distributed backends. |
| <xref:memoization> | Caches property getter and parameterless method results within the object instance. |
| <xref:immutability> | Enforces immutability constraints on types and their members. |
| <xref:observability> | Implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface with support for explicit properties, type inheritance, and child objects. |
| <xref:wpf> | Simplifies WPF development with command and dependency property aspects. |

> [!div class="see-also"]
> <xref:conceptual>
