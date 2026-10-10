// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0505.Fixed;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The member is introduced as a static member.
        builder.IntroduceMethod( nameof(Describe), scope: IntroductionScope.Static );
    }

    [Template]
    public string Describe()
    {
        return meta.Target.Type.Name;
    }
}
