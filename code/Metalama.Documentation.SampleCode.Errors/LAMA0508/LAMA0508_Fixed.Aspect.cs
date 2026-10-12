// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0508.Fixed;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The static member is not marked as sealed.
        builder.IntroduceMethod( nameof(GetTypeDescription) );
    }

    [Template]
    public static string GetTypeDescription()
    {
        return meta.Target.Type.Name;
    }
}
