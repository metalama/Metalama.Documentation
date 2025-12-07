---
uid: introspection-api
summary: "Namespaces for querying projects and solutions using the Metalama code model, including compilation process output."
keywords: "Metalama code model, querying projects, querying solutions, compilation process output, aspect instances, reported diagnostics, C# project, LINQPad, introspection"
created-date: 2023-01-26
modified-date: 2025-11-30
level: 300
---

# Introspection API

Query your projects and solutions using the Metalama code model. These namespaces provide access to compilation results and intermediate objects from the Metalama compilation process, such as aspect instances and reported diagnostics.

| Namespace                             | Description                                                                                                                |
|---------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Introspection> | Exposes the output of the Metalama compilation process. |
| <xref:Metalama.Framework.Workspaces> | Lets you load a C# project or solution in your custom code (or with LINQPad) and inspect the code model. |
| <xref:Metalama.LinqPad> | Implements a LINQPad driver and custom object dumpers. |

For conceptual documentation, see <xref:introspection>.

> [!div class="see-also"]
> <xref:introspection>
> <xref:Metalama.Framework.Workspaces>
