// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0507.Error;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // A static member cannot be virtual.
        builder.IntroduceMethod(
            nameof(GetDescription),
            scope: IntroductionScope.Static,
            buildMethod: method => method.IsVirtual = true );
    }

    [Template]
    public string GetDescription()
    {
        return meta.Target.Type.Name;
    }
}
