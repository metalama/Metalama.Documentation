// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0505.Error;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The template is an instance method, so the introduced member is an instance member.
        builder.IntroduceMethod( nameof(Describe) );
    }

    [Template]
    public string Describe()
    {
        return meta.Target.Type.Name;
    }
}
