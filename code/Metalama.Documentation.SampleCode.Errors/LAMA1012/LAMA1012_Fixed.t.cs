using System;
namespace Doc.LAMA1012.Fixed;
internal static class PriceCalculator
{
  public static decimal ApplyDiscount(decimal price) => price * 0.9m;
}
internal static class DiscountedPriceCalculator
{
  public static decimal ApplyDiscount(decimal price) => price * 0.8m;
}
internal static class Checkout
{
  public static void Run()
  {
    // The call is in a delegate, so Metalama can intercept it.
    Func<decimal> function = () => DiscountedPriceCalculator.ApplyDiscount(100m);
    Console.WriteLine(function());
  }
}