---
uid: custom-metrics
level: 400
summary: "Learn how to create and consume custom metrics using the Metalama SDK."
keywords: "custom metrics, Metalama SDK, create metric public API, implement metric, .NET, IMetric interface, SyntaxMetricProvider, MetalamaPlugInAttribute"
created-date: 2023-02-20
modified-date: 2025-12-02
---

# Custom metrics

Custom metrics let you measure and report on code characteristics beyond Metalama's built-in metrics. This article explains how to create custom metrics using the Metalama SDK and how to consume them in aspects, fabrics, or the introspection API.

## Creating a custom metric

### Step 1. Reference the Metalama SDK

Reference `Metalama.Framework.Sdk` and `Metalama.Framework`, both privately:

```xml
<PackageReference Include="Metalama.Framework.Sdk" Version="$(MetalamaVersion)" PrivateAssets="all" />
<PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" PrivateAssets="all" />
```

### Step 2. Create the metric public API

Create a `struct` that implements the <xref:Metalama.Framework.Metrics.IMetric`1> generic interface. The type parameter should be the type of declaration to which the metric applies (e.g., `IMemberOrNamedType`, `INamespace`, or `ICompilation`). Your metric `struct` can implement several generic instances of the <xref:Metalama.Framework.Metrics.IMetric`1> interface simultaneously.

Typically, your metric `struct` has at least one public property and an internal method to update the values, which the metric implementation uses.

#### Example

The following example demonstrates a single-value metric.

```cs
public struct SyntaxNodesCount : IMetric<IMemberOrNamedType>, IMetric<INamespace>, IMetric<ICompilation>
{
    public int Value { get; internal set; }

    internal void Add( in SyntaxNodesCount other ) => this.Value += other.Value;
}
```

### Step 3. Create the metric implementation

A metric requires several implementation classes:

1. Create a public class that derives from <xref:Metalama.Framework.Engine.Metrics.SyntaxMetricProvider`1>, where `T` is the metric type created above. In the constructor, pass an instance of the visitor created in the next step.

2. Inside the metric provider class, create a nested visitor class that derives from <xref:Metalama.Framework.Engine.Metrics.SyntaxMetricProvider`1.BaseVisitor>. Override the relevant `Visit` methods in this class. This class is the actual metric implementation. The visitor should recursively compute the metric for each syntax node in the syntax tree. The metric provider invokes the visitor for each member. The visitor shouldn't implement aggregation at the type or namespace level.

3. Implement the <xref:Metalama.Framework.Engine.Metrics.MetricProvider`1.Aggregate*> method. This method aggregates the metric from the level of members to the level of types, namespaces, or the whole project.

4. Annotate this class with the <xref:Metalama.Framework.Engine.MetalamaPlugInAttribute> custom attribute.

#### Example: counting syntax nodes

The following example implements a metric that counts the number of nodes in a member.

```cs
[MetalamaPlugIn]
public class SyntaxNodesCountMetricProvider : SyntaxMetricProvider<SyntaxNodesCount>
{
    public SyntaxNodesCountMetricProvider() : base( new Visitor() ) { }

    protected override void Aggregate( ref SyntaxNodesCount aggregate, in SyntaxNodesCount newValue )
        => aggregate.Add( newValue );

    private class Visitor : BaseVisitor
    {
        public override SyntaxNodesCount DefaultVisit( SyntaxNode node )
        {
            var metric = new SyntaxNodesCount { Value = 1 };

            foreach ( var child in node.ChildNodes() )
            {
                metric.Add( this.Visit( child ) );
            }

            return metric;
        }
    }        
}
```

### Step 4. Create an extension method for registration

To use your metric with the Introspection API (see <xref:introspection>), create an extension method on <xref:Metalama.Framework.Engine.Services.ServiceProviderBuilder`1> to register your metric provider. This provides a clean API for consumers:

```csharp
[CompileTime]
public static class MyMetricsExtensions
{
    public static void AddMyMetrics( this ServiceProviderBuilder<IProjectService> builder )
    {
        builder.Add<IMetricProvider<MyCustomMetric>>( _ => new MyCustomMetricProvider() );
    }
}
```

> [!NOTE]
> The type argument `T` of `Add<T>` must be the _interface_ type (e.g., `IMetricProvider<MyCustomMetric>`), not the concrete implementation type.

## Consuming a custom metric

### From an aspect or fabric

Use the metric as usual, for example, `declaration.Metrics().Get<MyCustomMetric>()`.

### From the Workspaces API

Consumers register your metrics using your extension method, then query as usual:

```csharp
// Register metric providers before loading the workspace.
WorkspaceCollection.Default.ServiceBuilder.AddMyMetrics();

// Load workspace and query metrics.
var workspace = await WorkspaceCollection.Default.LoadAsync( "MyProject.csproj" );
var metric = declaration.Metrics().Get<MyCustomMetric>();
```

See <xref:metrics> for details on using metrics with the Workspaces API.

> [!div class="see-also"]
>
> **See also**
>
> * <xref:sdk>
> * <xref:metrics>
> * <xref:introspection>
