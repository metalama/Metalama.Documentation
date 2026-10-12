// This is public domain Metalama sample code.

using System;
using System.Linq.Expressions;

namespace Doc.LAMA1012.Error;

internal static class PriceCalculator
{
    public static decimal ApplyDiscount( decimal price ) => price * 0.9m;
}

internal static class DiscountedPriceCalculator
{
    public static decimal ApplyDiscount( decimal price ) => price * 0.8m;
}

internal static class Checkout
{
    public static void Run()
    {
        // The call is in an expression tree, so Metalama cannot intercept it.
        Expression<Func<decimal>> expression = () => PriceCalculator.ApplyDiscount( 100m );

        Console.WriteLine( expression.Compile()() );
    }
}
