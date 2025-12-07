---
uid: linqpad
level: 300
summary: "This article explains how to use the Metalama driver for LINQPad to interactively inspect and query your source code."
keywords: "LINQPad, Metalama driver, inspect code, interactive queries, code exploration"
created-date: 2023-07-11
modified-date: 2025-11-30
---

# Using LINQPad

Metalama's LINQPad driver allows you to interactively query your source code as if it were a database. You can inspect declarations, dependencies, errors, warnings, aspect outcomes, and code transformations.

[LINQPad](https://www.linqpad.net/) is a popular tool for writing and executing LINQ queries against various data sources. With Metalama's driver, you extend this functionality to .NET projects and solutions.

> [!NOTE]
> The Metalama.LinqPad package is [open-source](https://github.com/metalama/Metalama/tree/HEAD/Metalama.LinqPad).

## Installing the Metalama driver

1. In the Explorer tool window, click **Add connection**.

    ![Install step 1](install-1.svg)

2. Click **View more drivers**.

     ![Install step 2](install-2.svg)

3. In the NuGet LINQPad Manager dialog:

    1. Select **Show all drivers**.
    2. Type _Metalama_.
    3. Select `Metalama.LinqPad` and click **Install**.
    4. Accept the disclaimers and wait, then click **Close**.

    ![Install step 3](install-3.svg)

## Opening a project or solution

1. In the Explorer tool window, click **Add connection**.

    ![Install step 1](install-1.svg)

    There are two Metalama drivers:

    * **Metalama Workspace**: Bound to a .NET project or solution, accessible through the `workspace` variable.
    * **Metalama Scratchpad**: Not bound to anything, so you load projects manually in your query.

2. Choose the **Metalama Workspace** or **Metalama Scratchpad** driver and click **Next**.

    ![Add connection 1](connection-1.svg)

3. If you chose **Metalama Workspace**, specify the path to the C# project or solution, then click **OK**.

    ![Add connection 2](connection-2.svg)

> [!WARNING]
> The `Metalama.LinqPad` driver version must be **equal to or higher than** the `Metalama.Framework` package version used in your projects.

## Querying source code

After adding a C# project or solution to LINQPad, you'll see this structure in the Explorer:

   ![Structure 1](explorer-1.svg)

The `workspace` variable allows you to query the entire workspace for all projects for all target frameworks.

For details on the workspace API, see <xref:workspaces>.

### Example queries

List all static fields:

```csharp
workspace.SourceCode.Fields.Where( f => f.IsStatic )
```

List all public methods in a specific project:

```csharp
workspace
  .GetProject( "MyProject" )
  .SourceCode
  .Methods
  .Where( m => m.Accessibility == Accessibility.Public )
```

## Permalinks

In the data grid view, declarations have a _permalink_ column. Clicking this link opens a new query that evaluates to that declaration using <xref:Metalama.Framework.Code.SerializableDeclarationId>.

```csharp
workspace.GetDeclaration(
  "MyProject",
  "net8.0",
  "F:MyNamespace.MyClass._myField",
  false );
```

## Using the Scratchpad driver

The **Metalama Scratchpad** driver doesn't require specifying a project in the connection. Instead, load projects in your query using <xref:Metalama.Framework.Workspaces.WorkspaceCollection>:

```csharp
var workspace = WorkspaceCollection.Default
    .Load( @"C:\src\MyProject\MyProject.csproj" );
```

This gives you the same `workspace` variable as the bound driver.

## See also

> [!div class="see-also"]
> <xref:workspaces>
> <xref:metrics>
> <xref:Metalama.Framework.Workspaces>
> <xref:Metalama.Framework.Introspection>
