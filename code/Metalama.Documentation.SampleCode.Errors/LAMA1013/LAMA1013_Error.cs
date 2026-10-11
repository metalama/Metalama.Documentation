// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1013.Error;

internal static class PriceCalculator
{
    public static decimal ApplyDiscount( decimal price ) => price * 0.9m;
}

internal class DiscountedPriceCalculator
{
    // The interceptor method must be static.
    public decimal ApplyDiscount( decimal price ) => price * 0.8m;
}

internal static class Checkout
{
    public static void Run() => Console.WriteLine( PriceCalculator.ApplyDiscount( 100m ) );
}
