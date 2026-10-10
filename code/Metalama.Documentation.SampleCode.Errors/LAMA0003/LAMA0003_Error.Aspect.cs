// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;

namespace Doc.LAMA0003.Error;

// This aspect targets types, but the class has no [AttributeUsage], so C# accepts it on any declaration.
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
