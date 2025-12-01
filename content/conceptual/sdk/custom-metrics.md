---
uid: custom-metrics
level: 400
summary: "The document provides a detailed guide on how to create and consume custom metrics using the Metalama SDK, including steps on referencing the SDK, creating the metric public API, and implementing the metric."
keywords: "custom metrics, Metalama SDK, create metric public API, implement metric, .NET, IMetric interface, SyntaxMetricProvider, MetalamaPlugInAttribute"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Custom metrics

## Creating a custom metric

### Step 1. Reference the Metalama SDK

Start by referencing `Metalama.Framework.Sdk` and `Metalama.Framework`, both privately:

```xml
<PackageReference Include="Metalama.Framework.Sdk" Version="$(MetalamaVersion)" PrivateAssets="all" />
<PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" PrivateAssets="all" />
```

### Step 2. Create the metric public API

Create a `struct` that implements the <xref:Metalama.Framework.Metrics.IMetric`1> generic interface. The type parameter should be the type of declaration to which the metric applies (e.g., `IMethod` or `IField`). Note that your metric `struct` can implement several generic instances of the <xref:Metalama.Framework.Metrics.IMetric`1> interface simultaneously.

Typically, your metric `struct` will have at least one public property. It will also have internal members to update the values, which will be utilized by the metric implementation.

#### Example

The following example demonstrates a single-value metric.

```cs
public struct SyntaxNodesCount : IMetric<IMethodBase>, IMetric<INamedType>, IMetric<INamespace>, IMetric<ICompilation>
{
    public int Value { get; internal set; }

    internal void Add( in SyntaxNodesCount other ) => this.Value += other.Value;
}
```

### Step 3. Create the metric implementation

A metric requires several implementation classes:

1. Create a public class that derives from <xref:Metalama.Framework.Engine.Metrics.SyntaxMetricProvider`1>, where `T` is the metric type created above. In the constructor, pass an instance of the visitor created in the next step.

2. Inside the metric provider class, create a nested visitor class that derives from <xref:Metalama.Framework.Engine.Metrics.SyntaxMetricProvider`1.BaseVisitor>. Override the relevant `Visit` methods in this class. This class forms the actual implementation of the metric. The visitor should recursively compute the metric for each syntax node in the syntax tree. The visitor is invoked by the metric provider (described below) for each _member_. The visitor should not implement aggregation at the type or namespace level.

3. Implement the <xref:Metalama.Framework.Engine.Metrics.MetricProvider`1.Aggregate*> method. This method is used to aggregate the metric from the level of members to the level of types, namespaces, or the whole project.

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

## Consuming a custom metric

Custom metrics can be consumed in the usual manner.

[comment]: # (TODO: what does "as usual" mean? a link or a short explanation would be useful)

[comment]: # (TODO: Testing a custom metric)

> [!div class="see-also"]
>
> **See also**
>
> <xref:sdk>
> <xref:metrics>
> <xref:introspection>

