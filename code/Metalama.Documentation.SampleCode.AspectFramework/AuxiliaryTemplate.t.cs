using System;
using System.Collections.Concurrent;
namespace Doc.AuxiliaryTemplate;
public class SelfCachedClass
{
  [Cache]
  public int Add(int a, int b)
  {
    var cacheKey = $"Add({string.Join(", ", new object[] { a, b })})";
    if (_cache.TryGetValue(cacheKey, out var returnValue))
    {
      string cacheKey_1 = cacheKey;
      global::System.Object? value = returnValue;
    }
    else
    {
      string cacheKey_2 = cacheKey;
      returnValue = a + b;
      _cache.TryAdd(cacheKey, returnValue);
    }
    return (int)returnValue;
  }
  [CacheAndLog]
  public int Rmove(int a, int b)
  {
    var cacheKey = $"Rmove({string.Join(", ", new object[] { a, b })})";
    if (_cache.TryGetValue(cacheKey, out var returnValue))
    {
      string cacheKey_1 = cacheKey;
      global::System.Object? value = returnValue;
      Console.WriteLine($"Cache hit: {cacheKey_1} => {value}");
    }
    else
    {
      string cacheKey_2 = cacheKey;
      Console.WriteLine($"Cache hit: {cacheKey_2}");
      returnValue = a - b;
      _cache.TryAdd(cacheKey, returnValue);
    }
    return (int)returnValue;
  }
  private readonly ConcurrentDictionary<string, object?> _cache = new();
}