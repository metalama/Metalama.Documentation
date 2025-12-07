---
uid: observability-standard-cases
summary: "Learn the code generation patterns for automatic properties, explicitly-implemented properties, field-backed properties, derived types, and child objects."
level: 200
keywords: "Metalama.Patterns.Observability, automatic properties, explicitly-implemented properties, field-backed properties, class inheritance, OnPropertyChanged method, ObservableAttribute, INotifyPropertyChanged, child objects, MVVM architectures"
created-date: 2024-06-11
modified-date: 2025-11-30
---

# Understanding Observable aspect scenarios

The `Metalama.Patterns.Observability` package supports the following scenarios:

- Automatic properties
- Explicitly-implemented properties whose getter references:
  - Fields
  - Other properties
  - Non-virtual instance methods
- Child objects—properties whose getter references properties of another object, referred to as a child object, stored in a field or an automatic property of the current type (if this child object is itself observable)
- Class inheritance

In this section, we present the code generation patterns for each supported scenario.

## Automatic properties

For automatic properties, the pattern is straightforward—the automatic property is transformed into a field-backed property. A new `OnPropertyChanged` method is introduced unless it already exists.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/Simple.cs]

## Explicitly-implemented properties

The <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect analyzes the dependencies between all properties in the type and calls the `OnPropertyChanged` method for computed properties (also known as read-only properties).

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ComputedProperty.cs]

## Field-backed properties

Mutable fields are converted into properties of the same name, and the setter of the new property calls `OnPropertyChanged` for all relevant dependent properties.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/FieldBacked.cs]

## Derived types

When a derived type has a property whose getter references a property of the base type, the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect overrides the `OnPropertyChanged` method, filters the property name, and recursively calls the `OnPropertyChanged` method for all dependent properties.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/Derived.cs]

## Child objects

In MVVM architectures, it's common for a property of the ViewModel to depend on a property of the Model object, which itself is a field or property of the ViewModel object.

When a property getter references a property of an object stored in another field or property (referred to as a child object), the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect generates a `SubscribeTo` method for the property containing the child object. This method subscribes to the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event of the child object.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ChildObject.cs tabs="target"]

## Child objects and derived types

The complexity increases when a type depends on a property of a property of the base type. To support this scenario, the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect generates two additional methods in the base type: `OnChildPropertyChanged` and `OnObservablePropertyChanged`.

The `OnChildPropertyChanged` method is called when a property of a child object has changed. Its role is to prevent derived classes from having to monitor the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event for their own needs. Instead, they can override the method and add their own logic. This method is generated only if the base type itself has a dependency on a child property.

The `OnObservablePropertyChanged` method is called when a property implementing `INotifyPropertyChanged` has changed. Compared to the standard `OnPropertyChanged`, this method supplies the old value of the property to allow child classes to subscribe and unsubscribe from the PropertyChanged event. When `OnChildPropertyChanged` is generated, `OnObservablePropertyChanged` may be redundant.

Both methods are annotated with the <xref:Metalama.Patterns.Observability.ObservedExpressionsAttribute?text=[ObservedExpressions]>. They form a contract between the base type and the derived types and inform the derived types about the properties for which the methods will be invoked.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ChildObject_Derived.cs  tabs="target"]

> [!div class="see-also"]
> <xref:observability>
