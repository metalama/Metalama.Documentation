---
uid: patterns
summary: "Metalama Patterns are libraries of design patterns for C#, developed by the Metalama team, and are available on GitHub under the MIT license."
level: 100
keywords: "design patterns, C#, Metalama Patterns"
created-date: 2024-06-11
modified-date: 2025-11-30
---

# Metalama Patterns

Metalama Patterns, housed under the <xref:patterns-api?text=Metalama.Patterns> namespace, consist of libraries of aspects that implement the most common design patterns for C#.

Constructed by the Metalama team, these patterns uphold the same quality standard as the Metalama framework itself.

> [!NOTE]
> Metalama Patterns are released under the open-source MIT license and are available on [GitHub](https://github.com/metalama/Metalama.Patterns).

The following libraries are currently available:

| Library | Description |
|---------|-------------|
| <xref:dependency-injection-aspect> | Although this package is primarily designed to make it easy for aspect authors to integrate dependency injection, it also contains an aspect that automatically pulls dependencies from fields and properties. Supports lazy loading. |
| <xref:contract-patterns> | Implements Contract-Based Programming via preconditions, postconditions, and invariants. This aids in building maintainable, reliable, and scalable software systems. |
| <xref:caching> | Combines object-oriented and aspect-oriented APIs.
| <xref:memoization> | Implements a simple and high-performance alternative to caching limited to get-only properties and paramerless methods. |
| <xref:immutability> | Represents the concept of Immutable Type.
| <xref:observability> | Contains an aspect that implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface. Supports explicit properties, type inheritance, and child objects. |
| <xref:wpf> | Two aspects to simplify the work with WPF: Command and Dependency Property. |

> [!div class="see-also"]
> <xref:conceptual>
