---
uid: introspection-api
summary: "The document provides information about namespaces in the Metalama code model that allow querying of projects and solutions, including the compilation process output."
keywords: "Metalama code model, querying projects, querying solutions, compilation process output, aspect instances, reported diagnostics, Metalama Framework, C# project, LinqPad, inspect code model"
created-date: 2023-01-26
modified-date: 2024-08-04
---

# Introspection API documentation

The following namespaces allow you to query your projects and solutions using the Metalama code model. This includes results and intermediate objects from the Metalama compilation process, such as aspect instances and reported diagnostics.

| Namespace                             | Description                                                                                                                |
|---------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Introspection> | This namespace exposes the output of the Metalama compilation process. |
| <xref:Metalama.Framework.Workspaces> | This namespace enables you to load a C# project or solution in your custom code (or with LINQPad) and inspect the code model. |
| <xref:Metalama.LinqPad> | This namespace implements a LINQPad driver and custom object dumpers. |

For conceptual documentation, see <xref:introspection>.



