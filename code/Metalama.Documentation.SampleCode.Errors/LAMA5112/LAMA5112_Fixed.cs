// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects;
using System.Linq;

namespace Doc.LAMA5112.Fixed;

public partial class PriceCatalog
{
    [Cache]
    public decimal GetPrice( string productId ) => productId.Length * 10m;

    public decimal GetStock( string productId ) => productId.Length * 3m;

    private sealed class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // GetStock has no custom attribute, so the aspect is applied only once.
            amender
                .Select( t => t.Methods.Single( m => m.Name == nameof(GetStock) ) )
                .AddAspect( m => new CacheAttribute() );
        }
    }
}
