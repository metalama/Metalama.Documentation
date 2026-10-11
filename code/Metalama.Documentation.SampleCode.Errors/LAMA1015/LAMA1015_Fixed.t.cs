using System;
namespace Doc.LAMA1015.Fixed;
internal static class PriceChangeHandler
{
  public static void OnPriceChanged(decimal price) => Console.WriteLine($"The price changed to {price}.");
}
internal static partial class Checkout
{
  // The partial method has an implementation.
  static partial void OnPriceChanged(decimal price);
  static partial void OnPriceChanged(decimal price) => Console.WriteLine(price);
  public static void Run() => PriceChangeHandler.OnPriceChanged(10m);
}