// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.OverrideMethodAspect_;

[AttributeUsage( AttributeTargets.Method )]
public abstract class OverrideMethodAspect : Attribute, IAspect<IMethod>
{
    public virtual void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        builder.Override( nameof(this.OverrideMethod) );
    }

    public virtual void BuildEligibility( IEligibilityBuilder<IMethod> builder )
    {
        builder.ExceptForInheritance().MustNotBeAbstract();
    }

    [Template]
    public abstract dynamic? OverrideMethod();
}