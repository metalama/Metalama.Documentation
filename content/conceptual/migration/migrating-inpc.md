---
uid: migrating-inpc
keywords: "PostSharp Metalama migration, NotifyPropertyChanged, INotifyPropertyChanged, NotifyPropertyChangedAttribute, "
---

# Migrating the [NotifyPropertyChanged] aspects

Metalama's equivalent to PostSharp's `[NotifyPropertyChanged]` aspect is the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]>. For details, refer to <xref:observability>.

Metamama's implementation strategy of the pattern is completely different than PostSharp's one. Where PostSharp maintained an in-memory dependency graph at run time, Metalama does most of the work at build time and doesn't maintain complex data structures at run time. 


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
* The `PropertyChanged` events cannot be implemented as weak events, i.e. they hold references to its handlers.
* Suppression of false positives is not implemented, i.e. the `PropertyChanged` event can be signaled even when there is no change in the property.
