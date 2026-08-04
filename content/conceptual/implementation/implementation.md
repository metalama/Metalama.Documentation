---
uid: implementation
level: 400
summary: This section covers Metalama's internal implementation details, including the compilation pipeline, aspect composition, serialization, and execution order.
keywords: "Metalama implementation, compilation pipeline, aspect composition, serialization, execution order, fabrics"
created-date: 2023-12-11
modified-date: 2026-08-04
---

# Under the hood

This section describes how Metalama works internally. You don't need any of it to write aspects, but it helps when you
have to explain a build behavior, diagnose a performance problem, or extend Metalama itself.

| Article | Description |
|---------|-------------|
| <xref:packages> | Lists the NuGet packages that constitute Metalama and their dependency graphs. |
| <xref:compile-time-dependencies> | Explains how Metalama determines which APIs are available to compile-time code, and how that result is cached and configured. |
| <xref:aspect-serialization> | Explains how Metalama serializes aspects whose effects cross project boundaries. |
| <xref:pipeline> | Describes the steps of the compilation pipeline, at compile time and at design time. |
| <xref:aspect-composition> | Explains how Metalama composes several aspects applied to the same declaration. |
| <xref:fabrics-execution-order> | Gives the execution order of project, transitive, namespace, and type fabrics. |

> [!div class="see-also"]
> <xref:aspects>
> <xref:fabrics>
