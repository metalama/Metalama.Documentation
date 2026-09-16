// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.InvalidateCompileTimeErrors.BadArgument;

public sealed class ProductCatalogue
{
    [Cache]
    public decimal GetPrice( string productId ) => throw new NotImplementedException();

    [InvalidateCache( nameof(GetPrice) )]
    public void UpdatePrice( string product, decimal price ) => throw new NotImplementedException();
}