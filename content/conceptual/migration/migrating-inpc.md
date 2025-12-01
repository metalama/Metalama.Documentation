---
uid: migrating-inpc
summary: "This article explains how to migrate PostSharp's [NotifyPropertyChanged] aspect to Metalama's [Observable] aspect, including API mapping and feature gaps."
keywords: "PostSharp Metalama migration, NotifyPropertyChanged, INotifyPropertyChanged, NotifyPropertyChangedAttribute, "
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Migrating the [NotifyPropertyChanged] aspects

Metalama's equivalent to PostSharp's `[NotifyPropertyChanged]` aspect is the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]>. For details, refer to <xref:observability>.

Metalama's implementation strategy of the pattern is completely different than PostSharp's one. Where PostSharp maintained an in-memory dependency graph at run time, Metalama does most of the work at build time and doesn't maintain complex data structures at run time.


## API mapping

Most features of PostSharp's `[NotifyPropertyChanged]` aspect are available in Metalama under a different name:

| PostSharp                        | Metalama                                                   |
| -------------------------------- | ---------------------------------------------------------- |
| `NotifyPropertyChangedAttribute` | <xref:Metalama.Patterns.Observability.ObservableAttribute> |
| `PureAttribute`                  | <xref:Metalama.Patterns.Observability.ConstantAttribute> |
| `SafeForDependencyAnalysisAttribute` | <xref:Metalama.Patterns.Observability.SuppressObservabilityWarningsAttribute> or `#pragma warning disable` |
| `IgnoreAutoChangeNotificationAttribute`  | <xref:Metalama.Patterns.Observability.NotObservableAttribute> |
| `INotifyChildPropertyChanged` | `OnChildPropertyChanged` protected method.

## Feature gaps

The following features haven't been implemented in Metalama yet:

* You cannot implement the `INotifyPropertyChanging` interface.
* The `PropertyChanged` events cannot be implemented as weak events, i.e., they hold references to their handlers.
* Suppression of false positives is not implemented, i.e., the `PropertyChanged` event can be signaled even when there is no change in the property.

> [!div class="see-also"]
>
> **Other migration topics**
>
> * <xref:benefits-over-postsharp>
> * <xref:differences-from-postsharp>
> * <xref:migration-feature-status>
> * <xref:migrating-aspects>
> * <xref:migrating-configuration>
>
> **Observability documentation**
>
> * <xref:observability>
