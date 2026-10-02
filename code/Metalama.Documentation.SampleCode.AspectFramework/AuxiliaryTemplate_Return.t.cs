using System.Collections.Concurrent;
namespace Doc.AuxiliaryTemplate_Return;
public class SelfCachedClass
{
  [Cache]
  public int Add(int a, int b)
  {
    var cacheKey = $"Add({string.Join(", ", new object[] { a, b })})";
    string cacheKey_1 = cacheKey;
    if (_cache.TryGetValue(cacheKey_1, out var returnValue))
    {
      return (int)returnValue;
    }
    int returnValue_1;
    returnValue_1 = a + b;
    string cacheKey_2 = cacheKey;
    global::System.Object? returnValue_2 = returnValue_1;
    _cache.TryAdd(cacheKey_2, returnValue_2);
    return returnValue_1;
  }
  private readonly ConcurrentDictionary<string, object?> _cache = new();
}