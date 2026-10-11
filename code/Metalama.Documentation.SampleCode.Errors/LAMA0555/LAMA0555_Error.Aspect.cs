// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0555.Error;

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

        // A union cannot hold an automatic property, because it would need a field.
        builder.With( result.Declaration ).IntroduceAutomaticProperty( "Label", typeof(string) );
    }
}
