// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects;
using System.Linq;

namespace Doc.LAMA5112.Error;

public partial class PriceCatalog
{
    [Cache]
    public decimal GetPrice( string productId ) => productId.Length * 10m;

    private sealed class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // The [Cache] aspect is already applied to GetPrice by the custom attribute.
            amender
                .Select( t => t.Methods.Single( m => m.Name == nameof(GetPrice) ) )
                .AddAspect( m => new CacheAttribute() );
        }
    }
}
