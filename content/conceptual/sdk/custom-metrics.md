---
uid: custom-metrics
level: 400
---

# Custom metrics

> [!WARNING]
> This feature is not completed and does not work as documented.

## Creating a custom metric

### Step 1. Create the projects in the solution

Start by creating the scaffolding as described in <xref:sdk-scaffolding>.

### Step 2. Create the metric public API

In the public project, create a `struct` that implements the <xref:Metalama.Framework.Metrics.IMetric`1> generic interface. The type parameter should be the type of declaration to which the metric applies (e.g., `IMethod` or `IField`). Note that your metric `struct` can implement several generic instances of the <xref:Metalama.Framework.Metrics.IMetric`1> interface simultaneously.

Typically, your metric `struct` will have at least one public property. It will also have internal members to update the values, which will be utilized by the metric implementation.

#### Example

The following example demonstrates a single-value metric.

```cs
public struct SyntaxNodeNumberMetric : IMetric<IMethodBase>, IMetric<INamedType>, IMetric<INamespace>, IMetric<ICompilation>
{
    public int Value { get; internal set; }

    internal void Add( in SyntaxNodeNumberMetric other ) => this.Value += other.Value;
}
```

### Step 3. Create the metric implementation

A metric requires several implementation classes. All of these must be contained in the weaver project created in Step 1.

1. Create a visitor class that derives from <xref:Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1>, where `T` is the metric type created above. Override the relevant `Visit` methods in this class. This class forms the actual implementation of the metric. The visitor should recursively compute the metric for each syntax node in the syntax tree. The visitor is invoked by the metric provider (described below) for each _member_. The visitor should not implement aggregation at the type or namespace level.

2. Create a public class that derives from <xref:Metalama.Framework.Engine.Metrics.SyntaxMetricProvider`1>, where `T` is again the same metric type. In the class constructor, pass an instance of the visitor created in the previous step.

3. Implement the <xref:Metalama.Framework.Engine.Metrics.MetricProvider`1.Aggregate*> method. This method is used to aggregate the metric from the level of members to the level of types, namespaces, or the whole project.

4. Annotate this class with the <xref:Metalama.Compiler.MetalamaPlugInAttribute> custom attribute.

#### Example: counting syntax nodes

The following example implements a metric that counts the number of nodes in a member.

```cs
    [MetalamaPlugIn]
    public partial class SyntaxNodeNumberMetricProvider : SyntaxMetricProvider<SyntaxNodeNumberMetric>
    {
        public SyntaxNodeNumberMetricProvider() : base( Visitor.Instance ) { }

        protected override void Aggregate( ref SyntaxNodeNumberMetric aggregate, in SyntaxNodeNumberMetric newValue )
           => aggregate.Add( newValue );

        private class Visitor : CSharpSyntaxVisitor<SyntaxNodeNumberMetric>
        {
            public static readonly Visitor Instance = new();

            public override SyntaxNodeNumberMetric DefaultVisit( SyntaxNode node )
            {
                var metric = new SyntaxNodeNumberMetric { Value = 1 };

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
