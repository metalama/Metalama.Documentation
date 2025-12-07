---
uid: patterns-api
summary: "Common design patterns for caching, contracts, observability, immutability, memoization, and WPF."
keywords: "Metalama patterns, caching, contracts, observability, INotifyPropertyChanged, immutability, memoization, WPF, dependency property, command"
created-date: 2023-12-11
modified-date: 2025-11-30
level: 200
---

# Patterns API

These namespaces implement common design patterns you can apply to your code using Metalama aspects.

| Namespace                             | Description                                                                                                                |
|---------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Patterns.Caching> | Implements method result caching with support for cache invalidation, dependencies, and distributed caching backends. |
| <xref:Metalama.Patterns.Contracts> | Provides precondition and postcondition contracts (e.g., `[NotNull]`, `[NotEmpty]`, `[Range]`) for parameter and return value validation. |
| <xref:Metalama.Patterns.Observability> | Automatically implements `INotifyPropertyChanged` and `INotifyPropertyChanging` interfaces for observable properties. |
| <xref:Metalama.Patterns.Immutability> | Enforces immutability constraints on types and their members. |
| <xref:Metalama.Patterns.Memoization> | Caches the result of property getters or parameterless methods within the object instance. |
| <xref:Metalama.Patterns.Wpf> | Provides WPF-specific patterns including dependency properties and commands. |

For conceptual documentation, see <xref:patterns>.

> [!div class="see-also"]
> <xref:patterns>
