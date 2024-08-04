---
uid: migration-feature-status
summary: "The document provides an update on the migration of PostSharp features to Metalama, detailing what has been completed and what is still in progress."
keywords: "migration of PostSharp features, Metalama vs PostSharp, PostSharp vs Metalama, comparison"
---

# Status of the migration of PostSharp features to Metalama

## PostSharp Aspect Framework

The PostSharp Framework has been entirely ported to Metalama, with a few notable limitations:

* Methods from an external assembly cannot be intercepted; only those from the current project can be.
* The event of suspending and resuming an `async` state machine, as in PostSharp, cannot be advised. Specifically, the `await` keyword cannot be advised.
* The _raise_ semantic of an event cannot be intercepted, only the _add_ and _remove_ semantics can be.
* Some constructor-related advice types are not yet implemented:

    * After the last constructor
    * After MemberwiseClone

## PostSharp Architecture Framework

The equivalent of PostSharp Architecture Framework (e.g. the `PostSharp.Constraints` namespace) is the `Metalama.Extensions.Architecture` package. 

See <xref:validation> for details.

## PostSharp Patterns

| Library                | Migration Status | Documentation            | Feature gaps                                                       |
| ---------------------- | ---------------- | ------------------------ | ------------------------------------------------------------------ |
| Contracts              | Completed        | <xref:contract-patterns> | None                                                               |
| Caching                | Completed        | <xref:caching>           | None                                                               |
| INotifyPropertyChanged | Completed        | <xref:observability>     | See <xref:migrating-inpc>        |
| WPF                   | Completed        | <xref:wpf>              | None                                                               |
| Undo/Redo              | Not started      |                          |                                                                    |
| Diagnostics (logging)  | Not started      |                          |                                                                    |
| Multi-threading        | Not started      |                          |                                                                    |
| Aggregatable           | Not started      |                          |                                                                    |
| Weak event             | Not started      |                          |                                                                    |


