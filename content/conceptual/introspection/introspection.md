---
uid: introspection
level: 300
summary: "This section explains how to query and analyze your source code using Metalama's introspection APIs, including the Workspaces API and LINQPad integration."
keywords: "introspection, code analysis, Metalama, Workspaces API, LINQPad, code queries, source code analysis"
created-date: 2025-11-30
modified-date: 2025-11-30
---

# Querying and analyzing code

Metalama provides powerful APIs for querying and analyzing your source code outside of aspects and fabrics. You can inspect declarations, dependencies, aspect outcomes, and code transformations either programmatically or interactively using LINQPad.

| Article | Description |
|---------|-------------|
| <xref:workspaces> | How to use the Workspaces API to query code programmatically from any .NET application. |
| <xref:linqpad> | How to use the Metalama driver for LINQPad to interactively query and explore your codebase. |
| <xref:metrics> | How to consume code metrics like statement counts and syntax node counts. |

## Benefits

The introspection APIs allow you to:

* Query code using the same <xref:Metalama.Framework.Code> API as aspects and fabrics.
* List target declarations of aspects.
* Inspect aspect outcomes: code transformations, diagnostics, and child aspects.
* Query the transformed code after Metalama executes.
* Analyze code dependencies and references.

## See also

> [!div class="see-also"]
> <xref:Metalama.Framework.Workspaces>
> <xref:Metalama.Framework.Introspection>
