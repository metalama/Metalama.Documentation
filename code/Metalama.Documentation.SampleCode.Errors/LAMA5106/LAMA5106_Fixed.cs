// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5106.Fixed;

public sealed partial class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _prices = new() { ["corn"] = 100 };

    [Cache]
    public decimal GetPrice( string productId ) => this._prices[productId];

    // Fixed: nameof guarantees that the method exists.
    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( string productId, decimal price ) => this._prices[productId] = price;
}
