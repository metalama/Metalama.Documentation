// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0033.Error;

public class DescribeAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceMethod( nameof(this.Describe) );
    }

    // Error: the method is used as a template but has no [Template] attribute.
    public string Describe() => "This object is enhanced by the [Describe] aspect.";
}
