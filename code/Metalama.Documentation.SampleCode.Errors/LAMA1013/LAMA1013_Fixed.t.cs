using System;
namespace Doc.LAMA1013.Fixed;
internal static class PriceCalculator
{
  public static decimal ApplyDiscount(decimal price) => price * 0.9m;
}
internal static class DiscountedPriceCalculator
{
  // The interceptor method is static.
  public static decimal ApplyDiscount(decimal price) => price * 0.8m;
}
internal static class Checkout
{
  public static void Run() => Console.WriteLine(DiscountedPriceCalculator.ApplyDiscount(100m));
}