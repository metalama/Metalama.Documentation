// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5101.Error;

public sealed class ProductCatalogue
{
    internal static readonly Dictionary<string, decimal> Prices = new() { ["corn"] = 100 };

    // The cache key of GetPrice includes the ProductCatalogue instance.
    [Cache]
    public decimal GetPrice( string productId ) => Prices[productId];
}

public sealed class PriceUpdater
{
    // Error: PriceUpdater doesn't derive from ProductCatalogue, so 'this' can't be mapped.
    [InvalidateCache( typeof(ProductCatalogue), nameof(ProductCatalogue.GetPrice) )]
    public void UpdatePrice( string productId, decimal price ) => ProductCatalogue.Prices[productId] = price;
}
