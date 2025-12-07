// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

// Specify that Log runs before Cache, so logging wraps around caching.
[assembly: AspectOrder( AspectOrderDirection.RunTime, typeof(Doc.LogAndCache.LogAttribute), typeof(Doc.LogAndCache.CacheAttribute) )]

namespace Doc.LogAndCache;

public partial class PriceCalculator
{
    // Both [Log] and [Cache] are applied to the same method.
    // AspectOrder ensures Log executes first at run time (outermost).
    [Log]
    [Cache]
    public decimal GetPrice( string productId )
    {
        Console.WriteLine( $"  Computing price for {productId}..." );

        return productId.Length * 10m;
    }
}
