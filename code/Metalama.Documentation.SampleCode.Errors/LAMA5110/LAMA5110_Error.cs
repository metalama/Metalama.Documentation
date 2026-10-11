// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;

namespace Doc.LAMA5110.Error;

public class PriceCatalog
{
    // Error: the method is static, but caching uses dependency injection by default.
    [Cache]
    public static decimal GetPrice( string productId ) => productId.Length * 10m;
}
