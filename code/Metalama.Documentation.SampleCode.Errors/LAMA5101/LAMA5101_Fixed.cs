// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5101.Fixed;

public sealed class ProductCatalogue
{
    internal static readonly Dictionary<string, decimal> Prices = new() { ["corn"] = 100 };

    // Fixed: the cache key of GetPrice no longer includes the ProductCatalogue instance.
    [Cache( IgnoreThisParameter = true )]
    public decimal GetPrice( string productId ) => Prices[productId];
}

public sealed class PriceUpdater
{
    [InvalidateCache( typeof(ProductCatalogue), nameof(ProductCatalogue.GetPrice) )]
    public void UpdatePrice( string productId, decimal price ) => ProductCatalogue.Prices[productId] = price;
}
