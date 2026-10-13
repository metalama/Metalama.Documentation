// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0555.Fixed;

public class GenerateResultAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var result = builder.IntroduceUnion(
            "OrderResult",
            u =>
            {
                u.Accessibility = Accessibility.Public;
                u.AddCase( typeof(int) );
                u.AddCase( typeof(string) );
            } );

        // A property whose accessor has a body holds no field, so a union accepts it.
        builder.With( result.Declaration ).IntroduceProperty( nameof(Label) );
    }

    [Template]
    public string Label
    {
        get { return "Order result"; }
    }
}
