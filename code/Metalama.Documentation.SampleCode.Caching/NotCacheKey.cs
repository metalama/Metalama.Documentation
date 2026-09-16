// This is public domain Metalama sample code.

using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.NotCacheKey;

public class PricingService
{
    [Cache]
    public decimal GetProductPrice( string productId, [NotCacheKey] string? correlationId )
        => throw new NotImplementedException();

    [Cache]
    public string[] GetProducts( string productId, [NotCacheKey] string? correlationId )
        => throw new NotImplementedException();
}