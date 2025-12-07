---
uid: metrics
level: 300
summary: "This article explains how to consume code metrics in Metalama to analyze code complexity from aspects, fabrics, or the Workspaces API."
keywords: "metrics, code metrics, Metalama, code complexity, StatementsCount, SyntaxNodesCount, LinesOfCode, IMeasurable, IMetric, aspects, fabrics, Workspaces"
created-date: 2025-11-30
modified-date: 2025-12-02
---

# Consuming code metrics

Code metrics provide quantitative measures of your source code, such as statement counts or syntax node counts. You can consume metrics in two scenarios:

* **Aspects and fabrics**: Make decisions based on code complexity at compile time.
* **Workspaces API**: Analyze code complexity from standalone applications or tools.

## Built-in metrics

The `Metalama.Extensions.Metrics` package provides three ready-to-use metrics:

| Metric | Description |
|--------|-------------|
| <xref:Metalama.Extensions.Metrics.StatementsCount> | Counts statements in a declaration. More relevant than line counts, but less accurate than syntax nodes for modern expression-oriented C#. |
| <xref:Metalama.Extensions.Metrics.SyntaxNodesCount> | Counts all syntax nodes in a declaration's syntax tree. Provides a more accurate measure of code complexity. |
| <xref:Metalama.Extensions.Metrics.LinesOfCode> | Counts lines of code with three sub-metrics: `Logical` (excludes braces and comments), `NonBlank` (non-whitespace content), and `Total` (total line span). |

You can apply all metrics to methods, constructors, types, namespaces, and the entire compilation. When applied to a container (type, namespace, compilation), the metric aggregates values from all contained members.

## Using metrics in aspects and fabrics

### Step 1. Add the NuGet package

Add the `Metalama.Extensions.Metrics` package to your project:

```xml
<PackageReference Include="Metalama.Extensions.Metrics" Version="$(MetalamaVersion)" />
```

### Step 2. Get the metric value

Call the <xref:Metalama.Framework.Metrics.MetricsExtensions.Metrics*> extension method on any declaration, then call <xref:Metalama.Framework.Metrics.Metrics`1.Get*> with the metric type:

```csharp
var syntaxNodeCount = method.Metrics().Get<SyntaxNodesCount>();
int count = syntaxNodeCount.Value;
```

Use this in:

* **Aspects**: In `BuildAspect` to conditionally add advice, or in templates to embed metric values in generated code.
* **Fabrics**: In `AmendProject` or `AmendType` to filter declarations based on complexity.

### Example: adding logging to complex methods

The following example uses a fabric to add logging only to methods with more than 50 syntax nodes. The `Add` method is simple and remains unchanged, while the `Fibonacci` method exceeds the threshold and gets logging injected.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Metrics/LogComplexMethods.cs]

## Using metrics with the Workspaces API

When using the <xref:Metalama.Framework.Workspaces> API from a standalone application, you need to register the metric providers manually before loading a workspace.

### Step 1. Add NuGet packages

Add both the `Metalama.Framework.Workspaces` and `Metalama.Extensions.Metrics` packages:

```xml
<PackageReference Include="Metalama.Framework.Workspaces" Version="$(MetalamaVersion)" />
<PackageReference Include="Metalama.Extensions.Metrics" Version="$(MetalamaVersion)" />
```

### Step 2. Register metric providers

Before loading the workspace, call the <xref:Metalama.Extensions.Metrics.ServiceBuilderExtensions.AddMetrics*> extension method on <xref:Metalama.Framework.Workspaces.WorkspaceCollection.ServiceBuilder>:

[!code-csharp[](~/code/Metalama.Documentation.SampleCode.Metrics.Workspaces/Program.cs#RegisterMetricProviders)]

### Step 3. Load the workspace and query metrics

After registering the providers, load your workspace and query metrics as usual:

[!code-csharp[](~/code/Metalama.Documentation.SampleCode.Metrics.Workspaces/Program.cs#QueryMetrics)]

### Example: code complexity analyzer

The following example demonstrates a standalone tool that analyzes code complexity:

[!code-csharp[](~/code/Metalama.Documentation.SampleCode.Metrics.Workspaces/Program.cs)]

## See also

> [!div class="see-also"]
> <xref:Metalama.Framework.Metrics>
> <xref:Metalama.Extensions.Metrics.StatementsCount>
> <xref:Metalama.Extensions.Metrics.SyntaxNodesCount>
> <xref:Metalama.Extensions.Metrics.LinesOfCode>
> <xref:Metalama.Extensions.Metrics.ServiceBuilderExtensions.AddMetrics*>
> <xref:custom-metrics>
> <xref:workspaces>
