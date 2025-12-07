---
uid: same-type-multiple-instances
level: 300
summary: "This document explains how to handle multiple instances of the same aspect type applied to the same declaration, including primary/secondary instance selection, accessing secondary instances, and common patterns for merging aspect configuration."
keywords: "multiple aspects, same declaration, SecondaryInstances, primary instance, secondary instance, aspect merging, IAspectInstance, meta.AspectInstance, builder.AspectInstance"
created-date: 2025-12-01
modified-date: 2025-12-01
---

# Multiple instances of the same aspect type

When multiple instances of the same aspect type are applied to the same declaration, Metalama uses a _primary/secondary instance_ model. Only one instance (the _primary_) is executed, but it has access to all other instances (the _secondary instances_) and can incorporate their configuration.

## When does this happen?

Multiple instances of the same aspect type can be applied to a declaration through various sources:

1. Custom attribute: An aspect applied directly via an attribute like `[MyAspect]`. Multiple custom attributes appear as different aspect instances.
2. Fabrics: An aspect added programmatically by a fabric.
3. Child aspects: An aspect added by another aspect using the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound> property.
4. Required aspects: An aspect added by another aspect using the `builder.RequireAspect` method.
5. Inherited aspects: An aspect inherited from a base class or interface when marked with <xref:Metalama.Framework.Aspects.InheritableAttribute>.

When the same aspect type is applied from multiple sources, or multiple times from the same source, Metalama must determine which instance to execute.

## Primary instance selection

The _primary instance_ is selected based on proximity to the target declaration. The selection criteria, from highest to lowest priority, are:

1. Aspects defined using a custom attribute
2. Aspects added by another aspect (child aspects)
3. Aspects required by another aspect
4. Aspects inherited from another declaration
5. Aspects added by a fabric

Within each category, the ordering is currently undefined, which can lead to non-deterministic behavior if your aspect relies on that ordering.

## Accessing secondary instances

Secondary instances are accessible through the <xref:Metalama.Framework.Aspects.IAspectInstance.SecondaryInstances> property. You can access this property from:

- `builder.AspectInstance.SecondaryInstances` in the `BuildAspect` method
- `meta.AspectInstance.SecondaryInstances` in template methods

Each secondary instance provides access to:

- The aspect instance itself (via the `Aspect` property), which you can cast to your aspect type to read its properties.
- Information about what added this instance, such as an attribute, fabric, or parent aspect (via the `Predecessors` property).

## Common patterns

When your aspect encounters secondary instances, you can handle them in several ways. The most common patterns are merging configuration from all instances, or reporting a warning when duplicates are detected.

### Merging configuration

A common approach is to merge all aspect instances into a single one. The merging logic is specific to each aspect.

The following example demonstrates a logging aspect that merges categories from all instances (primary and secondary) when multiple instances are applied to the same method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/MultipleInstances.cs tabs="aspect,fabric,target,transformed"]

In this example, the `PlaceOrder` method has two `[Log]` instances: one from the custom attribute with `Category="Orders"`, and one from the fabric with `Category="Monitoring"`. The aspect merges both categories, resulting in `[Monitoring, Orders]` in the log output. The `GetOrderStatus` method has only one instance from the fabric, showing just `[Monitoring]`.

### Reporting warnings on duplicates

You can warn users when they've applied the same aspect multiple times unintentionally. In this example, a warning is reported when `GetOrderStatus` has `[Log]` from both the custom attribute and the fabric.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/SecondaryInstancesWarning.cs tabs="aspect,fabric,target,transformed"]

## Relationship to predecessors

Don't confuse <xref:Metalama.Framework.Aspects.IAspectInstance.SecondaryInstances> with <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors>:

- `SecondaryInstances` are other instances of the _same_ aspect type on the _same_ target. These are "siblings."
- `Predecessors` are the artifacts (attributes, fabrics, parent aspects) that _created_ this aspect instance. These are "parents."

For example, if `[ParentAspect]` adds a child `[ChildAspect]` to a method, and you also apply `[ChildAspect]` via a custom attribute:

- The custom attribute instance is the _primary_ (higher priority)
- The child aspect instance is a _secondary instance_
- The _predecessor_ of the child aspect instance is `[ParentAspect]`

> [!div class="see-also"]
> <xref:ordering-aspects>
> <xref:child-aspects>
> <xref:aspect-inheritance>
> <xref:fabrics-adding-aspects>
> <xref:Metalama.Framework.Aspects.IAspectInstance>

## Excluding aspects from specific targets

In some cases, you may want to prevent an aspect from being applied entirely, rather than handling multiple instances.

To completely prevent an aspect from being applied to a specific declaration (regardless of fabrics or other sources), use the built-in <xref:Metalama.Framework.Aspects.ExcludeAspectAttribute> attribute:

```csharp
// Prevents any [Log] aspect from being applied to this method
[ExcludeAspect(typeof(LogAttribute))]
public void GetOrderStatus(string orderId)
{
    // This method will not be logged, even if a fabric adds [Log]
}
```
