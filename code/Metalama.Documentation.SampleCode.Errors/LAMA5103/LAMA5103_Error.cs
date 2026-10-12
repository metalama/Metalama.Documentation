// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA5103.Error;

public sealed class ProductCatalogue
{
    private readonly Dictionary<string, decimal> _prices = new() { ["42"] = 100 };

    [Cache]
    public decimal GetPrice( string productId ) => this._prices[productId];

    // Error: an int can't be converted to the string parameter of GetPrice.
    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( int productId, decimal price ) => this._prices[productId.ToString()] = price;
}
