---
uid: sdk
level: 400
summary: "Use the Metalama.Framework.Sdk NuGet package for advanced scenarios like creating custom compile-time services and aspect weavers. For conventional development, use Metalama.Framework."
keywords: "Metalama.Framework.Sdk, Roslyn-based APIs, low-level access, Metalama.Framework, Roslyn API, aspect weavers, custom metrics"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Extending Metalama with the Roslyn API

The `Metalama.Framework.Sdk` NuGet package provides direct, low-level access to Metalama using [Roslyn-based APIs](https://learn.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

`Metalama.Framework.Sdk` is more complex and less secure than `Metalama.Framework`. Use it only for creating specialized coding aids. For conventional development, use `Metalama.Framework`.

| Article | Description |
|---------|-------------|
| <xref:aspect-weavers> | Create aspect weavers that perform arbitrary transformations on C# code using the low-level Roslyn API. |
| <xref:roslyn-api> | Access the syntax tree in the Roslyn API from aspects. |
| <xref:custom-metrics> | Create and consume custom metrics. |
| <xref:sdk-extensions> | Create SDK extension projects that provide custom services aspects can consume at compile time. |

> [!div class="see-also"]
>
> **See also**
>
> * <xref:conceptual>
> * <xref:aspects>
> * <xref:advanced-api>
