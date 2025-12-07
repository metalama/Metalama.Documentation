---
uid: multiple-instances
level: 300
summary: "This document provides an overview of handling multiple aspect instances applied to the same declaration, covering both ordering of different aspect types and handling multiple instances of the same type."
keywords: "multiple aspects, aspect ordering, SecondaryInstances, primary instance, secondary instance, aspect execution order"
created-date: 2025-12-01
modified-date: 2025-12-01
---

# Handling multiple aspect instances

When multiple aspects are applied to the same declaration, Metalama must determine how to handle them. There are two distinct scenarios:

## Multiple aspect types on the same declaration

When you apply _different_ aspect types to the same declaration (for example, `[Log]` and `[Cache]` on the same method), Metalama determines the _execution order_ at run time. This is configured using the <xref:Metalama.Framework.Aspects.AspectOrderAttribute>.

For details, see <xref:ordering-aspects>.

## Multiple instances of the same aspect type

When you apply _multiple instances of the same_ aspect type to the same declaration (for example, via a custom attribute and a fabric), Metalama selects one instance as the _primary_ and exposes the others as _secondary instances_. The primary instance can then access and merge configuration from secondary instances.

For details, see <xref:same-type-multiple-instances>.

> [!div class="see-also"]
> <xref:ordering-aspects>
> <xref:same-type-multiple-instances>
> <xref:child-aspects>
> <xref:aspect-inheritance>
> <xref:Metalama.Framework.Aspects.IAspectInstance>
