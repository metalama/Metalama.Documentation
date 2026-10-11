// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Doc.LAMA0556.Error;

public class GenerateRecordAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var record = builder.IntroduceRecord(
            "OrderLine",
            buildRecord: r =>
            {
                r.Accessibility = Accessibility.Public;
                r.AddPositionalParameter( "Quantity", typeof(int) );
            } );

        var printMembers = record.Declaration.Methods.OfName( "PrintMembers" ).Single();

        // Error: the compiler synthesizes PrintMembers, so there is no declaration on which to write the attribute.
        builder.With( printMembers ).IntroduceAttribute( AttributeConstruction.Create( typeof(ExcludeFromCodeCoverageAttribute) ) );
    }
}
