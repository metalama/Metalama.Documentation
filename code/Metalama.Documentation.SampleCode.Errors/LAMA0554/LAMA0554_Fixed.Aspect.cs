// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0554.Fixed;

public class IntroduceResultAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceUnion(
            "Result",
            u =>
            {
                u.Accessibility = Accessibility.Public;

                // The union declares its cases.
                u.AddCase( typeof(int) );
                u.AddCase( typeof(string) );
            } );
    }
}
