---
uid: child-aspects
level: 300
summary: "The document explains how to add child aspects in Metalama Framework, the conditions they must follow, and how to access the parent aspect."
keywords: "child aspects, parent aspect, Metalama Framework, IAspectBuilder, AddAspect method, aspect order, IQuery, RequireAspect method, aspect precedence, auditing aspects"
created-date: 2023-02-17
modified-date: 2024-08-04
---

# Adding child aspects

An aspect can introduce other aspects to child declarations. These aspects are known as _child aspects_. Child aspects must adhere to two conditions:

* The child aspect class should be processed _after_ the parent aspect class. In other words, the child aspect class must be listed _before_ the parent class in the <xref:Metalama.Framework.Aspects.AspectOrderAttribute> ordering definition. Please refer to <xref:ordering-aspects> for more details.
* The target declaration of the child aspect class must be contained in the target declaration of the parent aspect. For example, a type-level aspect can introduce aspects to methods of the current type, but not to methods of a different type. A parameter-level aspect can add a child aspect to the parent method or another method of the same type, but not of a different type.

An aspect can add child aspects from the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method by using the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound?text=builder.Outbound> property, followed by calling the <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> method. To introduce child aspects on members of the current declaration, use the <xref:Metalama.Framework.Fabrics.IQuery`1.Select*> or <xref:Metalama.Framework.Fabrics.IQuery`1.SelectMany*> method, and then invoke <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> or <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspectIfEligible*>.

## Overriding a child aspect with an attribute.

Note that any aspect added "manually" as a custom attribute takes precedence over aspects added by the parent aspect. In this case, the _primary instance_ of the aspect is the one created by the custom attribute, and the _secondary instance_ is the one added by the parent aspect. Secondary aspect instances are exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.SecondaryInstances?text=IAspectInstance.SecondaryInstances> property, which you can access from <xref:Metalama.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectInstance?text=builder.AspectInstance>.


## Example: audited object and audited member

The following example contains two aspects: `[AuditedObject]` and `[AuditedMember]`. The `[AuditedObject]` aspect automatically audits all public methods and properties. `[AuditedMember]` only audits the target method or property. To avoid duplicating logic between `AuditedObjectAttribute` and `AuditedMemberAttribute`, `AuditedObjectAttribute` adds instances of the `AuditedObjectAttribute` class as _child aspects_.

`[AuditedMember]` also allows developers to _opt out_ of auditing.  If the developer adds `[AuditedMember(false)]` to a member, this aspect instance will take precedence over the one added by `[AuditedObject]`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/ChildAspect.cs name="ChildAspect"]


## Accessing the parent aspect from the child aspect

Parent aspects are enumerated in the <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors> property, accessible by the child aspect from <xref:Metalama.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectInstance?text=builder.AspectInstance>.


## Requiring an aspect without creating a new instance

Instead of calling <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*>, you can use <xref:Metalama.Framework.Aspects.AspectQueryExtensions.RequireAspect*>. This method is generic, and its type parameter must be set to an aspect type with a default constructor. It checks if the target declaration already contains an aspect of the required type, and if not, it adds a new aspect instance. 

If you were using <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> and the aspect was already present, a new aspect instance would be created, a primary aspect instance would be chosen, and the other instances would be made available as secondary instances. With <xref:Metalama.Framework.Aspects.AspectQueryExtensions.RequireAspect*>, there would be no secondary instance, but the parent aspect would be exposed as a _predecessor_ in the <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors> collection.

