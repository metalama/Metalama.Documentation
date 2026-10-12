// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5102.Error;

public sealed class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _prices = new() { ["corn"] = 100 };

    [Cache]
    public decimal GetPrice( string productId ) => this._prices[productId];

    // Error: UpdatePrice has no parameter named 'productId'.
    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( string product, decimal price ) => this._prices[product] = price;
}
