---
uid: workspaces
level: 300
summary: "This article explains how to use the Metalama Workspaces API to programmatically query and analyze source code from any .NET application."
keywords: "Workspaces API, Metalama, code analysis, source code queries, IProjectSet, code introspection, programmatic code analysis"
created-date: 2025-11-30
modified-date: 2025-11-30
---

# Using the Workspaces API

The <xref:Metalama.Framework.Workspaces> API allows you to query and analyze source code programmatically from any .NET application. You can load projects or solutions, inspect declarations, analyze dependencies, and examine the results of Metalama transformations.

## Step 1. Add the NuGet package

Add the `Metalama.Framework.Workspaces` package to your project:

```xml
<PackageReference Include="Metalama.Framework.Workspaces" Version="$(MetalamaVersion)" />
```

## Step 2. Load a project or solution

Use <xref:Metalama.Framework.Workspaces.WorkspaceCollection.Load*> to load a project or solution:

```csharp
using Metalama.Framework.Workspaces;

var workspace = WorkspaceCollection.Default.Load( @"C:\src\MyProject\MyProject.csproj" );
```

You can also use <xref:Metalama.Framework.Workspaces.Workspace.Load*> directly:

```csharp
var workspace = Workspace.Load( @"C:\src\MySolution.sln" );
```

## Step 3. Query the code model

The <xref:Metalama.Framework.Workspaces.Workspace> object exposes the <xref:Metalama.Framework.Workspaces.IProjectSet> interface with the following properties:

* <xref:Metalama.Framework.Workspaces.IProjectSet.SourceCode>: Access source code **before** Metalama executes. For instance, `workspace.SourceCode.Types` lists all types in the workspace.

* <xref:Metalama.Framework.Introspection.IIntrospectionCompilationResult.TransformedCode>: Code **after** Metalama executes, including introduced declarations.

* <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.Diagnostics>: Errors, warnings, and messages reported by the C# compiler, Metalama, or aspects.

* <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.AspectClasses>, <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.AspectLayers>, <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.AspectInstances>, <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.Advice>, and <xref:Metalama.Framework.Introspection.IIntrospectionCompilationDetails.Transformations>: Different steps of the Metalama pipeline.

> [!NOTE]
> If your projects target multiple frameworks, declarations appear multiple times in queries—once per target framework.

## Filtering projects

### Querying a single project

Use <xref:Metalama.Framework.Workspaces.Workspace.GetProject*> to query a single project by name (without extension):

```csharp
workspace
  .GetProject( "MyProject" )
  .SourceCode
  .Fields
  .Where( f => f.IsStatic )
```

### Getting a project subset

Use <xref:Metalama.Framework.Workspaces.IProjectSet.GetSubset*> with a predicate to filter projects:

```csharp
workspace
  .GetSubset( p => p.TargetFramework == "net8.0" )
  .SourceCode
  .Types
```

### Applying workspace filters

Use <xref:Metalama.Framework.Workspaces.Workspace.ApplyFilter*> to apply filters directly to the `workspace` object:

```csharp
workspace.ApplyFilter( p => p.TargetFramework == "net8.0" );

// All subsequent queries use the filter
var types = workspace.SourceCode.Types;

// Clear filters when done
workspace.ClearFilters();
```

## Inspecting code references

Query inbound and outbound references using <xref:Metalama.Framework.Workspaces.DeclarationExtensions.GetInboundReferences*> and <xref:Metalama.Framework.Workspaces.DeclarationExtensions.GetOutboundReferences*>:

* **Inbound references**: References to the declaration.
* **Outbound references**: References from the declaration.

```csharp
var field = workspace.SourceCode.Types
    .SelectMany( t => t.Fields )
    .First( f => f.Name == "_myField" );

// Get all code referencing this field
var references = field.GetInboundReferences();
```

## Using declaration permalinks

Declarations can be uniquely identified using <xref:Metalama.Framework.Code.SerializableDeclarationId>. Use <xref:Metalama.Framework.Workspaces.Workspace.GetDeclaration*> to retrieve a declaration by its ID:

```csharp
var declaration = workspace.GetDeclaration(
    "MyProject",
    "net8.0",
    "F:MyNamespace.MyClass._myField",
    false );
```

## Reporting diagnostics

The <xref:Metalama.Framework.Workspaces.DiagnosticReporter.Report*> extension method allows you to report diagnostics directly from LINQ queries. Use it with <xref:Metalama.Framework.Workspaces.DiagnosticReporter> to track reported warnings and errors:

```csharp
// Report violations and track counts
workspace.SourceCode.Types
    .Where( t => t.Name.StartsWith( "_" ) )
    .Report( Severity.Warning, "NAMING001", "Type names should not start with underscore." );

Console.WriteLine( $"Found {DiagnosticReporter.ReportedWarnings} warnings." );
```

## Example: architecture verification

The following example demonstrates an architecture verification tool that checks naming conventions and namespace dependencies:

[!code-csharp[](~/code/Metalama.Documentation.SampleCode.Workspaces/Program.cs)]

## See also

> [!div class="see-also"]
> <xref:Metalama.Framework.Workspaces>
> <xref:Metalama.Framework.Introspection>
> <xref:linqpad>
> <xref:metrics>
