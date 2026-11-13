---
uid: mcp-server
level: 300
summary: "This article explains how to let coding agents query your source code with the Metalama MCP server, a local Model Context Protocol server that runs C# queries over the Metalama code model."
keywords: "MCP, Model Context Protocol, coding agent, AI agent, Claude Code, code queries, introspection, Metalama.Mcp"
created-date: 2026-11-13
modified-date: 2026-11-13
---

# Using the MCP server

The Metalama MCP server lets coding agents explore your source code the way you do with <xref:linqpad>. An agent loads a project or solution once, then runs C# queries over the same code model as aspects and fabrics: declarations, references, aspect instances, diagnostics, metrics, and the code transformed by Metalama.

The server implements the [Model Context Protocol](https://modelcontextprotocol.io) (MCP), so it works with any agent that supports MCP, such as Claude Code, GitHub Copilot or Cursor.

> [!NOTE]
> The `Metalama.Mcp` package is [open-source](https://github.com/metalama/Metalama/tree/HEAD/Metalama.LinqPad/src/Metalama.Mcp).

## Installing the server

The server is the `metalama-mcp` .NET tool from the `Metalama.Mcp` package. It requires the .NET 10 SDK or later.

1. Install the tool:

    ```powershell
    dotnet tool install -g Metalama.Mcp
    ```

2. Register the `metalama-mcp` command with your agent. With Claude Code:

    ```powershell
    claude mcp add metalama -- metalama-mcp
    ```

    Other agents take the same command in their MCP configuration file. Start the agent from the directory of your repository, because the server resolves relative paths against its working directory.

The command accepts two options:

| Option | Description |
|--------|-------------|
| `--output-dir <directory>` | The directory where HTML results are written. The default is `metalama-mcp` in the temporary directory. |
| `--timeout <seconds>` | The default soft timeout of a query, 60 seconds by default. See [Timeouts and limits](#timeouts-and-limits). |

> [!WARNING]
> The `Metalama.Mcp` version must be **equal to or higher than** the `Metalama.Framework` package version used in your projects.

## Security

The server runs arbitrary C# code by design, with the privileges of the user who started it. It is not a sandbox.

* **The server is local.** It communicates only through its standard input and output, so only the agent that started it can use it. It never opens a network port and has no option to do so.
* **Loading a project runs its build logic.** Like a build, loading a workspace runs the MSBuild targets of the project and its Metalama aspects and fabrics. Only load code that you trust.
* **The analyzed code is data.** String literals and comments in the analyzed code can contain text aimed at the agent. This is the same risk as when the agent reads the files with its other tools.

## Tools

The server exposes five tools to the agent.

| Tool | Description |
|------|-------------|
| `load_workspace` | Loads one or more projects or solutions (`.csproj`, `.sln`, `.slnx`) and keeps them loaded for the session. Returns the projects, their target frameworks, and the load diagnostics. |
| `reload_workspace` | Reloads a workspace from disk after its files were edited. |
| `unload_workspace` | Frees the memory used by a workspace. |
| `query` | Runs a C# query and renders its result. |
| `open` | Renders the object that a link names. See [Links](#links). |

Agents receive the instructions of the server when they connect, so they usually need no further explanation. You can ask an agent questions such as "which public methods of the domain layer have more than five parameters?" or "which aspects apply to `OrderService`?".

The result of `load_workspace` lists the projects of the workspace as _scopes_ of the form `Project@tfm`:

```text
workspace: FrogHattery.csproj (loaded in 4.5 s)
base: metalama://FrogHattery.csproj/FrogHattery@net10.0/source/
projects:
  FrogHattery@net10.0    workspace.GetProject("FrogHattery", "net10.0")  Metalama enabled
load diagnostics: 0 error(s), 0 warning(s)
fingerprint: 2b7089a175ae
```

## Writing queries

A query is either a C# expression, whose value is rendered, or a block of statements that ends with `return`. Queries have the same API as LINQPad queries. For details, see <xref:workspaces>.

The following globals are available:

| Global | Description |
|--------|-------------|
| `workspace` | The loaded <xref:Metalama.Framework.Workspaces.Workspace>. |
| `workspaces` | All loaded workspaces, by key. |
| `D(id)`, `D<T>(id)` | Resolves a declaration identifier, such as `T:MyApp.Customer`, in the source code. Pass `project` and `tfm` when the workspace has several projects or target frameworks, and `transformed: true` to resolve it in the transformed code. |
| `Open(link)`, `Open<T>(link)` | Resolves a link printed by a previous result. |
| `ct` | A cancellation token that is canceled on the soft timeout. |

The namespaces imported by LINQPad queries are imported, as well as `System`, `System.Collections.Generic`, `System.Linq`, `System.Text` and `System.Threading.Tasks`. A query can use `await`.

For instance, the following query lists the types that derive from `Frog`:

```csharp
workspace.SourceCode.Types.Where( t => t.BaseType?.Name == "Frog" )
```

The agent receives this result:

```text
base: metalama://FrogHattery.csproj/FrogHattery@net10.0/source/
IEnumerable<INamedType> — 3 items
T:FrogHattery.TreeFrog        TreeFrog.cs:6
T:FrogHattery.PoisonDartFrog  PoisonDartFrog.cs:6
T:FrogHattery.BullFrog        BullFrog.cs:6
--- 775 ms · fingerprint 2b7089a175ae ---
```

Each declaration starts with its link and ends with its location, relative to the directory of the project, which the agent can open with its file tools.

A projection to an anonymous type is rendered as a table:

```csharp
workspace.SourceCode.Methods
    .Where( m => m.Parameters.Count > 1 )
    .Select( m => new { Type = m.DeclaringType.Name, m.Name, Parameters = m.Parameters.Count } )
```

```text
IEnumerable<{ Type, Name, Parameters }> — 5 items
Type                 Name                   Parameters
FrogPond             CompareFrogs           2
FrogHatShop          SellHat                2
FrogHatShop          FindHatsBySize         2
SeasonalFrogFashion  CalculateFashionScore  2
SeasonalFrogFashion  RankHatsForFrog        2
```

The server shows the same properties as the LINQPad driver: properties marked with <xref:Metalama.Framework.Utilities.HiddenAttribute> or <xref:System.ObsoleteAttribute> are not displayed. A query can still read them.

When a query writes to the console, its output is returned in a `--- console ---` section. Diagnostics that the query reports are returned in an `--- error list ---` section.

## Links

Every rendered object starts with a _link_: a stable identifier that the agent passes to the `open` tool, or to `Open(…)` or `D<T>(…)` in a query, to continue from that object. The server keeps no table of links: it resolves each link again from its text.

A link consists of an _anchor_ followed by _steps_:

* The anchor is usually a declaration identifier, such as `T:FrogHattery.BullFrog`, as returned by <xref:Metalama.Framework.Code.IDeclaration.ToSerializableId*>. It can also be `project`, `projects`, `diagnostics`, `aspect-instances`, `aspect-classes`, `advice` or `transformations`.
* Each step is a property name, such as `Methods`, or a zero-based index.

For instance, `open` with the link `T:FrogHattery.BullFrog/Methods` returns:

```text
IEnumerable<IMethod> — 9 items
M:FrogHattery.BullFrog.CatchFly                         BullFrog.cs:24
M:FrogHattery.BullFrog.ChallengeFrog(FrogHattery.Frog)  BullFrog.cs:41
…
```

Links are relative to the _base_ printed at the top of each result. The absolute form is `metalama://<workspace>/<project>@<tfm>/<source|transformed>/<anchor>/<steps>`.

After the agent edits files and calls `reload_workspace`, links to declarations remain valid as long as the declarations exist. Links that contain an index carry a fingerprint, such as `?v=2b7089a175ae`, and report `stale` after any reload, because the order of some collections of the code model can change between two loads.

## Viewing results in a browser

When you want to look at a result yourself, ask the agent to run the query with the `html` format. The server writes a self-contained HTML file and returns its path. The page shows the result as LINQPad does:

* Objects are property tables, collapsed to their header until you click them.
* Collections are grids with one column per property. You can sort them by a column and filter them.
* Values nested in a table are collapsed and expand in place.
* Each linked row has buttons to copy its link or its C# expression.

The page loads nothing from the network and runs no code from the result.

Agents that support MCP Apps, such as Claude, show the same view in the conversation. In this view, clicking a link calls the server to open the object.

The `json` format returns the same tree as JSON, for agents that process results further. Its schema is `result.schema.json` in the `Metalama.Mcp` package.

## Timeouts and limits

* A query is canceled through `ct` after its soft timeout, 60 seconds by default. A query that does not observe `ct` is stopped after twice that time: the server restarts its worker process and reloads the workspaces on the next call.
* A result shows 50 items per collection and one level of properties by default. The agent can change these limits with the `maxItems` and `maxDepth` arguments. The `html` format shows 500 items and three levels by default.
* A text result is limited to 20,000 characters.

## See also

> [!div class="see-also"]
> <xref:linqpad>
> <xref:workspaces>
> <xref:Metalama.Framework.Workspaces>
> <xref:Metalama.Framework.Introspection>
