---
uid: migrating-multicasting
summary: "The document provides instructions on migrating multicasting from PostSharp to Metalama. It details how to enable multicasting for simple and blank aspects, and how to implement eligibility requirements."
keywords: "multicasting PostSharp, multicasting Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Migrating PostSharp attribute multicasting to Metalama

Multicasting in PostSharp is a feature of all aspects that enables targeting several declarations using a single custom attribute or XML element in the `postsharp.config` configuration file. It's exposed by `MulticastAttribute`, the ultimate base type of all aspect classes.

In Metalama, multicasting isn't implemented as a core feature but as an extension. This decision was made because the goal of adding an aspect to multiple declarations is better achieved in Metalama using _fabrics_. You might eventually decide not to use multicasting. For more details, see <xref:fabrics-adding-aspects>.

Multicasting in Metalama is provided for backward compatibility with PostSharp. The objective is to _not_ require PostSharp users to change their _business code_ when migrating to Metalama, but only their _aspect code_. Multicasting in Metalama is implemented by the <xref:Metalama.Extensions.Multicast> namespace. The implementation of this namespace is [open source](https://github.com/metalama/Metalama/tree/HEAD/Metalama.Extensions/src/Metalama.Extensions.Multicast).

## Enabling multicasting for a simple aspect

If your aspect is based on <xref:Metalama.Framework.Aspects.OverrideMethodAspect> or <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect>, you can enable multicasting by changing the base class to <xref:Metalama.Extensions.Multicast.OverrideMethodMulticastAspect> or <xref:Metalama.Extensions.Multicast.OverrideFieldOrPropertyMulticastAspect> respectively. No other changes should be required.

The process is slightly more complex if your aspect is derived from another base class.

## Enabling multicasting for a blank aspect

Below are general instructions to add the multicasting feature to any aspect. You can verify these instructions by examining the [source code](https://github.com/metalama/Metalama/tree/HEAD/Metalama.Extensions/src/Metalama.Extensions.Multicast) of <xref:Metalama.Extensions.Multicast.OverrideMethodMulticastAspect> or <xref:Metalama.Extensions.Multicast.OverrideFieldOrPropertyMulticastAspect>.

### Step 1: Derive your class from MulticastAspect and implement IAspect\<T\> as appropriate

The simplest approach is for your aspect to derive from <xref:Metalama.Extensions.Multicast.MulticastAspect> instead of any other class.

The <xref:Metalama.Extensions.Multicast.MulticastAspect> class defines:

* All properties that simulate the `MulticastAttribute` class from PostSharp, such as <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeTargetTypes> or <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeTargetMemberAttributes>
* A protected property <xref:Metalama.Extensions.Multicast.MulticastAspect.Implementation> that you can call from your derived classes to implement multicasting

Your aspect must also implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface for all relevant types of declarations:

* On the _final_ declarations where the aspect is actually applied (performs some actual work)
* On any _intermediate_ declaration where the aspect does no work other than multicasting itself to select child declarations

The <xref:Metalama.Extensions.Multicast.MulticastAspect> class already implements the `IAspect<ICompilation>` and `IAspect<INamedType>` interfaces and correctly implements the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method. For the interfaces you implement yourself, you must implement `BuildAspect`.

### Step 2: Implement the BuildAspect methods

Your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method should _only_ call the <xref:Metalama.Extensions.Multicast.MulticastImplementation.BuildAspect*?text=this.Implementation.BuildAspect*> method. The arguments you need to pass depend on the kind of declaration of the implemented <xref:Metalama.Framework.Aspects.IAspect`1> interface:

* For _intermediate_ declarations, pass a single argument: the <xref:Metalama.Framework.Aspects.IAspectBuilder`1>.
* For _final_ declarations, pass the <xref:Metalama.Framework.Aspects.IAspectBuilder`1> _and_ a delegate that performs the actual work. This delegate is called _unless_ the aspect is skipped due to <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeExclude>.

Example:

```csharp
    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        this.Implementation.BuildAspect(
            builder,
            b => b.Override( nameof(this.TheTemplate) ) );
    }
```

### Step 3: Implement eligibility

If your aspect has eligibility requirements on the _type_ to which it's applied, override the <xref:Metalama.Extensions.Multicast.MulticastAspect.BuildEligibility*> method.

Instead of repeating this eligibility condition in the `BuildEligibility` method for all final destinations of the aspect, you can call the `BuildEligibility(INamedType)` method as follows:

```csharp
public override void BuildEligibility( IEligibilityBuilder<INamedType> builder )
{
    // Do not offer the aspect on static types.
    builder.MustNotBeStatic();
}

public void BuildEligibility( IEligibilityBuilder<IMethod> builder )
{
    // Include the conditions of the declaring type.
    this.BuildEligibility( builder.DeclaringType() );

    // Conditions that are specific to the method.
    builder.MustNotBeAbstract();
}
```

> [!div class="see-also"]
>
> * <xref:migration>
> * <xref:migrating-aspects>
> * <xref:fabrics-adding-aspects>
> * <xref:eligibility>
