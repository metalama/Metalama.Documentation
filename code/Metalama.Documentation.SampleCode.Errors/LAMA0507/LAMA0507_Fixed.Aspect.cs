// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0507.Fixed;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The member is introduced as an instance member, so it can be virtual.
        builder.IntroduceMethod(
            nameof(GetDescription),
            buildMethod: method => method.IsVirtual = true );
    }

    [Template]
    public string GetDescription()
    {
        return meta.Target.Type.Name;
    }
}
