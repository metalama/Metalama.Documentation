// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0554.Error;

public class IntroduceResultAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Error: the union declares no case.
        builder.IntroduceUnion( "Result", u => u.Accessibility = Accessibility.Public );
    }
}
