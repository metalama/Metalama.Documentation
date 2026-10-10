// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0033.Fixed;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.Describe) );
    }

    // Fixed: the method has the [Template] attribute.
    [Template]
    public string Describe() => "This object is enhanced by the [Describe] aspect.";
}
