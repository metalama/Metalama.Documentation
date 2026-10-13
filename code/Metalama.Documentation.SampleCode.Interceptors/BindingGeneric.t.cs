using System.Collections.Generic;
namespace Doc.BindingGeneric;
[TraceCache]
public class ProductCatalog
{
  public string? GetName(int id) => CacheTracing.Get<string>($"name:{id}");
  public decimal GetPrice(int id) => CacheTracing.Get<decimal>($"price:{id}");
}
public static class Cache
{
  private static readonly Dictionary<string, object?> _items = new();
  public static T? Get<T>(string key) => _items.TryGetValue(key, out var value) ? (T? )value : default;
}