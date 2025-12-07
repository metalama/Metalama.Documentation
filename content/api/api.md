---
uid: api
summary: "Information and guidelines for using the Metalama API."
keywords: "Metalama API, code model, aspect API, extensions, patterns, caching, introspection, SDK, testing, migration"
created-date: 2023-02-20
modified-date: 2025-11-30
level: 200
---

# Metalama API

The Metalama API provides comprehensive access to code transformation, analysis, and introspection capabilities for .NET applications.

| Section                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:code-api> | Defines the compile-time representation of the code model. |
| <xref:aspect-api> | Create aspects and fabrics that transform source code, report diagnostics, and validate declarations. |
| <xref:extensions-api> | Extensions based on the `Metalama.Framework` abstractions, including architecture validation, code fixes, and dependency injection. |
| <xref:patterns-api> | Implementation of common design patterns, including caching, memoization, code contracts, and observability (`INotifyPropertyChanged`). |
| <xref:flashtrace-api> | Logging front-end used by <xref:Metalama.Patterns.Caching>. |
| <xref:introspection-api> | Query and analyze your code using the Metalama code model from any application, including LINQPad. |
| <xref:advanced-api> | Extend Metalama using the Roslyn API. Use cases include analyzing method implementations, implementing custom aspect weavers, and defining custom metrics. |
| <xref:testing-api> | Testing frameworks for unit testing compile-time logic and snapshot-based aspect testing. |
| <xref:migration-api> | Maps the PostSharp API to its Metalama equivalent. |

> [!div class="see-also"]
> <xref:Metalama.Framework>
> <xref:advanced-api>
