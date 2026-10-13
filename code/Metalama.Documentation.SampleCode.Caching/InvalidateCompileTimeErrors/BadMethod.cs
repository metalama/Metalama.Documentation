// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.InvalidateCompileTimeErrors.BadMethod;

public sealed class ProductCatalogue
{
    [Cache]
    public decimal GetPrice( string productId ) => throw new NotImplementedException();

    [InvalidateCache( "GetBadPrice" )]
    public void UpdatePrice( string productId, decimal price )
        => throw new NotImplementedException();
}