// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA1015.Error;

internal static class PriceChangeHandler
{
    public static void OnPriceChanged( decimal price ) => Console.WriteLine( $"The price changed to {price}." );
}

internal static partial class Checkout
{
    // The partial method has no implementation, so the compiler omits the call.
    static partial void OnPriceChanged( decimal price );

    public static void Run() => OnPriceChanged( 10m );
}
