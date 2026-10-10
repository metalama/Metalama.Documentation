// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0003.Fixed;

// Fixed: [AttributeUsage] restricts the aspect to the kinds of declarations it supports.
[AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct )]
public class DescribeAttribute : Attribute, IAspect<INamedType>
{
    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.Describe) );
    }

    public void BuildEligibility( IEligibilityBuilder<INamedType> builder ) { }

    [Template]
    public string Describe() => meta.Target.Type.Name;
}
