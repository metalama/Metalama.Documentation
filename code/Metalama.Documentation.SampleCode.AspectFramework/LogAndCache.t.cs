using Metalama.Framework.Aspects;
using System;
using System.Collections.Concurrent;
namespace Doc.LogAndCache;
public partial class PriceCalculator
{
  // Both [Log] and [Cache] are applied to the same method.
  // AspectOrder ensures Log executes first at run time (outermost).
  [Log]
  [Cache]
  public decimal GetPrice(string productId)
  {
    Console.WriteLine("Entering PriceCalculator.GetPrice(string)");
    try
    {
      var key = $"PriceCalculator.GetPrice({string.Join(", ", new object[] { productId })})";
      if (_cache.TryGetValue(key, out var cached))
      {
        Console.WriteLine($"Cache hit for {key}");
        return (decimal)cached;
      }
      Console.WriteLine($"Cache miss for {key}");
      decimal result;
      Console.WriteLine($"  Computing price for {productId}...");
      result = productId.Length * 10m;
      _cache[key] = result;
      return result;
    }
    finally
    {
      Console.WriteLine("Leaving PriceCalculator.GetPrice(string)");
    }
  }
  private static readonly ConcurrentDictionary<string, object?> _cache = new();
}
