// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.ExcludeThisParameter;

[CachingConfiguration( IgnoreThisParameter = true )]
public class PricingService
{
    private readonly Guid _id = Guid.NewGuid();

    [Cache]
    public decimal GetProductPrice( string productId ) => throw new NotImplementedException();

    [Cache]
    public string[] GetProducts( string productId ) => throw new NotImplementedException();

    public override string ToString() => $"CurrencyService {this._id}";
}