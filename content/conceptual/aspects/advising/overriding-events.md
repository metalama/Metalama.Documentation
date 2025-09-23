---
uid: overriding-events
level: 300
summary: "The document discusses how to override events in a similar manner to overriding properties, but notes that overriding event invocation is not implemented."
keywords: "overriding events, .NET, add accessor, remove accessor, event invocation, Metalama Framework, OverrideEventAspect"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Overriding events

Metalama allows you to override the three semantics of events: _add_, _remove_, and _raise_.

To override an event, you can use one of the following approaches:

- Create an aspect class from the <xref:Metalama.Framework.Aspects.OverrideEventAspect> class and override the <xref:Metalama.Framework.Aspects.OverrideEventAspect.OverrideAdd*>,
<xref:Metalama.Framework.Aspects.OverrideEventAspect.OverrideRemove*>, and/and <xref:Metalama.Framework.Aspects.OverrideEventAspect.OverrideRaise*> methods.

- Use the <xref:Metalama.Framework.Advising.AdviserExtensions.OverrideAccessors*> method from the `BuildAspect` method.

## Overriding the _add_ and _remove_ semantics

Overriding the _add_ and _remove_ semantics events follows a similar process to [overriding properties](overriding-properties.md). 

If you attempt to override a field-like event, it is transformed into an explicitly-implemented event and its backing field.

### Example: logging

(demonstrates code transformation)


## Overriding the _raise_ semantic

 Most of the time, overriding an event involves overriding its _raise_ semantic. For instance, if you want to swallow exceptions in event handlers, or execute events in a background thread, it's best to do it overriding the _raise_ semantic.


### Example: exception handling


### Example: background

### Limitations

- delegate signatures with a non-`void` return type or with `out` and `ref` parameters
- using `meta.Target.Event.Raise()` from the `OverrideRaise` template (you must use `meta.Proceed()`),

