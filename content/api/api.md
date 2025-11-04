---
uid: api
summary: "This document provides information and guidelines on how to use the Metalama API."
created-date: 2023-02-20
modified-date: 2023-12-11
---

# Metalama API documentation



| Section                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:code-api> | Defines the compile-time representation of the code model. |
| <xref:aspect-api> | An API that allows you to transform the source code, report diagnostics, and more.
| <xref:extensions-api> | Useful of extensions based on the `Metalama.Framework` abstractions including architecture validation, code fixes, and dependency injection.
| <xref:patterns-api> | Implementation of common patterns including caching, memoization, code contracts, and observability (`INotifyPropertyChanged`).
| <xref:flashtrace-api> | A logging front-end used by <xref:Metalama.Patterns.Caching>. |
| <xref:introspection-api> | Allows you to use the <xref:code-api> from any application, including LINQPad.
| <xref:advanced-api> | Allows you to extend Metalama using Roslyn. Use cases include analyzing the method implementations, implementing custom aspect weavers, or additional metrics
| <xref:testing-api> | Two testing frameworks: one for unit testing, the second for snapshot-based testing.
| <xref:migration-api> | A documentation mapping the PostSharp API to its Metalama equivalent.


