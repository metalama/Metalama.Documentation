// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.AbsoluteExpiration_Attribute;

[CachingConfiguration( AbsoluteExpiration = 60 )]
public class PricingService
{
    [Cache]
    public decimal GetProductPrice( string productId ) => throw new NotImplementedException();

    [Cache( AbsoluteExpiration = 20 )]
    public string[] GetProducts( string productId ) => throw new NotImplementedException();
}