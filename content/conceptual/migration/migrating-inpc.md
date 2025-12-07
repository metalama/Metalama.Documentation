---
uid: migrating-inpc
summary: "This article explains how to migrate PostSharp's [NotifyPropertyChanged] aspect to Metalama's [Observable] aspect, including API mapping and feature gaps."
keywords: "PostSharp Metalama migration, NotifyPropertyChanged, INotifyPropertyChanged, NotifyPropertyChangedAttribute, "
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Migrating the [NotifyPropertyChanged] aspect

Metalama's equivalent to PostSharp's `[NotifyPropertyChanged]` aspect is the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]>. For details, see <xref:observability>.

Metalama's implementation strategy is completely different from PostSharp's. Where PostSharp maintained an in-memory dependency graph at runtime, Metalama does most of the work at build time and doesn't maintain complex data structures at runtime.

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

* You can't implement the `INotifyPropertyChanging` interface.
* The `PropertyChanged` events can't be implemented as weak events—they hold references to their handlers.
* Suppression of false positives isn't implemented—the `PropertyChanged` event can be signaled even when there's no change in the property.

> [!div class="see-also"]
>
> * <xref:benefits-over-postsharp>
> * <xref:differences-from-postsharp>
> * <xref:migration-feature-status>
> * <xref:migrating-aspects>
> * <xref:migrating-configuration>
> * <xref:observability>
