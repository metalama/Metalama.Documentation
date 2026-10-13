// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.DeclarationBuilders;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Doc.LAMA0556.Fixed;

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

        var quantity = record.Declaration.Properties.OfName( "Quantity" ).Single();

        // The attribute is written on the positional parameter, which is a declaration.
        builder.With( quantity ).IntroduceAttribute( AttributeConstruction.Create( typeof(ExcludeFromCodeCoverageAttribute) ) );
    }
}
