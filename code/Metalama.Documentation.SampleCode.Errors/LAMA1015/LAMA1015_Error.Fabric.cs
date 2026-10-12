// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.LAMA1015.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectTypes()
            .Where( t => t.Name == "Checkout" )
            .InterceptMethods(
                b => b
                    .Type( typeof(Checkout) )
                    .Methods( "OnPriceChanged" )
                    .RedirectToExistingMethod(
                        typeof(PriceChangeHandler).AsINamedType().Methods.OfName( "OnPriceChanged" ).Single() ) );
    }
}
