// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.Collections.Generic;
using System.Linq;

namespace Doc.LAMA5107.Fixed;

public sealed partial class ProductCatalogue
{
    // Maps product identifiers to categories.
    private readonly Dictionary<string, string> _categories = new() { ["corn"] = "cereals" };

    [Cache]
    public string[] GetProducts() => this._categories.Keys.ToArray();

    [Cache]
    public string[] GetProducts( string category )
        => this._categories.Where( p => p.Value == category ).Select( p => p.Key ).ToArray();

    // Fixed: the aspect explicitly invalidates all overloads of GetProducts.
    [InvalidateCache( nameof(GetProducts), AllowMultipleOverloads = true )]
    public void AddProduct( string productId, string category ) => this._categories.Add( productId, category );
}
