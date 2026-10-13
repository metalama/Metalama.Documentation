// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5103.Fixed;

public sealed class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _prices = new() { ["42"] = 100 };

    [Cache]
    public decimal GetPrice( string productId ) => this._prices[productId];

    // Fixed: the parameter has the same type as in GetPrice.
    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( string productId, decimal price ) => this._prices[productId] = price;
}
