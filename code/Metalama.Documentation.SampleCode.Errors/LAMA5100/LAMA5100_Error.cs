// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5100.Error;

public sealed class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _prices = new() { ["corn"] = 100 };

    // Error: GetPrice is not cached, so it can't be invalidated.
    public decimal GetPrice( string productId ) => this._prices[productId];

    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( string productId, decimal price ) => this._prices[productId] = price;
}
