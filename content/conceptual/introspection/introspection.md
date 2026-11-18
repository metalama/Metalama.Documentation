---
uid: introspection
level: 300
summary: "This section explains how to query and analyze your source code and its aspects: from the command line with metalama introspect, which describes the real build, or interactively with the MCP server, LINQPad and the Workspaces API, which load the code themselves."
keywords: "introspection, code analysis, metalama introspect, MCP, coding agents, Workspaces API, LINQPad, code queries, introspection file, mlsym"
created-date: 2025-11-30
modified-date: 2026-11-18
---

# Querying and analyzing code

Metalama lets you query your source code and its aspects outside of aspects and fabrics: which aspects apply to a declaration, what they change, which diagnostics they report, and how the code is structured. There are two families of tools. They differ in where their information comes from.

| Article | Description |
|---------|-------------|
| <xref:introspecting-from-command-line> | How to ask the `metalama introspect` command which aspects apply to the code and what they change, from the introspection file that the build writes. |
| <xref:mcp-server> | How to let coding agents run C# queries over the code model with the Metalama MCP server, and look at the results in your browser. |
| <xref:linqpad> | How to use the Metalama driver for LINQPad to query and explore your codebase interactively. |
| <xref:workspaces> | How to use the Workspaces API to query code programmatically from any .NET application. |
| <xref:metrics> | How to consume code metrics like statement counts and syntax node counts. |

## Choosing a tool

### The real build: metalama introspect

The `metalama introspect` command reads a file that the build of your project writes, the _introspection file_. Its answers therefore describe exactly what the build did: the same compiler, the same MSBuild properties, the same configuration and target frameworks, and the same version of Metalama as the build. The command itself never builds or loads the project, so it answers in a moment.

Use `metalama introspect` when you need to know what the build actually did with the aspects:

* Which aspects apply to a declaration, and what created them: an attribute, a fabric, inheritance, or another aspect.
* Which transformations the aspects make to a declaration, and in which order.
* Which diagnostics the aspects reported during the build.

It is the tool of choice for coding agents that work in a terminal and need to check, before they change a method, what the aspects do to it. Its limits follow from its source: it describes only the aspects and the declarations they touch, and only as of the last build. Build the project again after a change.

### Interactive exploration: the MCP server

The MCP server, like LINQPad and the Workspaces API, does not read the output of the build. It loads the project itself, in its own process, with the version of Metalama that it ships, and runs the aspects again. This is not the real build: when your project references another version of Metalama, or its build depends on the environment, the results can differ from what the build produced.

In exchange, these tools are interactive and see the whole code model, not only the aspects:

* They run any C# query over declarations, references, derived types, metrics, aspect instances, and the code before and after the transformations of Metalama.
* They answer immediately after you edit a file and reload the workspace, without a build.
* The MCP server shows results in your browser, as pages where you can expand any declaration, edit the query and run it again.

Use the MCP server to explore and understand a codebase with a coding agent, and LINQPad to do the same yourself. Use the Workspaces API to run such queries from your own programs and tests.

### Summary

| | `metalama introspect` | MCP server, LINQPad, Workspaces API |
|---|---|---|
| Source of the information | The introspection file written by the build | A workspace that the tool loads and compiles itself |
| Describes the real build | Yes | No: the same code, compiled again by the tool |
| Content | Aspects, their transformations and diagnostics | The whole code model, aspects included |
| Queries | Fixed collections, filtered with `--where` | Any C# query |
| After a change | Build the project again | Reload the workspace |
| Presentation | JSON or text in the terminal | Text, JSON, or interactive pages in the browser (MCP server, LINQPad) |

## See also

> [!div class="see-also"]
> <xref:introspecting-from-command-line>
> <xref:mcp-server>
> <xref:ai-agents>
> <xref:Metalama.Framework.Workspaces>
> <xref:Metalama.Framework.Introspection>
