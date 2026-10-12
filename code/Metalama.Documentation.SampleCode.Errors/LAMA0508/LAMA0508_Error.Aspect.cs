// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0508.Error;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // A static member cannot be sealed.
        builder.IntroduceMethod(
            nameof(GetTypeDescription),
            buildMethod: method => method.IsSealed = true );
    }

    [Template]
    public static string GetTypeDescription()
    {
        return meta.Target.Type.Name;
    }
}
