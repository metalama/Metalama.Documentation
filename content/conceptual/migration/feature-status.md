---
uid: migration-feature-status
summary: "The document provides an update on the migration of PostSharp features to Metalama, detailing what has been completed and what is still in progress."
keywords: "migration of PostSharp features, Metalama vs PostSharp, PostSharp vs Metalama, comparison"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Status of the migration of PostSharp features to Metalama

## PostSharp Aspect Framework

The PostSharp Framework has been entirely ported to Metalama, with a few notable limitations:

* Methods from an external assembly cannot be intercepted; only those from the current project can be.
* The event of suspending and resuming an `async` state machine, as in PostSharp, cannot be advised. Specifically, the `await` keyword cannot be advised.
* Some constructor-related advice types are not yet implemented:
    * After the last constructor
    * After MemberwiseClone

## PostSharp Architecture Framework

The equivalent of PostSharp Architecture Framework (e.g., the `PostSharp.Constraints` namespace) is the `Metalama.Extensions.Architecture` package.

See <xref:validation> for details.

## PostSharp Patterns

| Library                | Migration Status | Documentation            | Feature gaps                                                       |
| ---------------------- | ---------------- | ------------------------ | ------------------------------------------------------------------ |
| Contracts              | Completed        | <xref:contract-patterns> | None                                                               |
| Caching                | Completed        | <xref:caching>           | None                                                               |
| INotifyPropertyChanged | Completed        | <xref:observability>     | See <xref:migrating-inpc>                                          |
| WPF                    | Completed         | <xref:wpf>               | None                                                               |
| Undo/Redo              | Not planned      |                          |                                                                    |
| Diagnostics (logging)  | Not planned      |                          |                                                                    |
| Multi-threading        | Not planned      |                          |                                                                    |
| Aggregatable           | Not planned      |                          |                                                                    |
| Weak event             | Not planned      |                          |                                                                    |



> [!NOTE]
> Please contact us if you have an enterprise subscription and rely on a PostSharp feature that we don't plan to migrate to Metalama.

> [!div class="see-also"]
>
> **Other migration topics**
>
> * <xref:benefits-over-postsharp>
> * <xref:differences-from-postsharp>
> * <xref:migrating-aspects>
> * <xref:migrating-configuration>
> * <xref:migrating-inpc>
>
> **Pattern documentation**
>
> * <xref:contract-patterns>
> * <xref:caching>
> * <xref:observability>
> * <xref:wpf>
>
> **Architecture validation**
>
> * <xref:validation>
