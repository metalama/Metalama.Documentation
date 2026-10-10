using System;
using System.Reflection;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
namespace Doc.LAMA5110.Fixed;
public class PriceCatalog
{
  // Fixed: dependency injection is disabled for this method.
  [Cache(UseDependencyInjection = false)]
  public static decimal GetPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return GetPrice_Source((string)args[0]);
    }
    return ((ICachingService)CachingService.Default).GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, null, new object[] { productId }, Invoke);
  }
  private static decimal GetPrice_Source(string productId) => productId.Length * 10m;
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  static PriceCatalog()
  {
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(PriceCatalog).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'PriceCatalog.GetPrice(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
}