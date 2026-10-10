// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;

namespace Doc.LAMA5110.Fixed;

public class PriceCatalog
{
    // Fixed: dependency injection is disabled for this method.
    [Cache( UseDependencyInjection = false )]
    public static decimal GetPrice( string productId ) => productId.Length * 10m;
}
