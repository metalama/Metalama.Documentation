using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Doc.LAMA5101.Fixed;
public sealed class ProductCatalogue
{
  internal static readonly Dictionary<string, decimal> Prices = new()
  {
    ["corn"] = 100
  };
  // Fixed: the cache key of GetPrice no longer includes the ProductCatalogue instance.
  [Cache(IgnoreThisParameter = true)]
  public decimal GetPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetPrice_Source(string productId) => Prices[productId];
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetPrice(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = true, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}
public sealed class PriceUpdater
{
  [InvalidateCache(typeof(ProductCatalogue), nameof(ProductCatalogue.GetPrice))]
  public void UpdatePrice(string productId, decimal price)
  {
    ProductCatalogue.Prices[productId] = price;
    object result = null;
    _cachingService.Invalidate(_methodsInvalidatedBy_UpdatePrice_8158D76B6E0BFEAA70B5CB758CE36A82[0], this, new object[] { productId });
  }
  private ICachingService _cachingService;
  private static MethodInfo[] _methodsInvalidatedBy_UpdatePrice_8158D76B6E0BFEAA70B5CB758CE36A82;
  static PriceUpdater()
  {
    _methodsInvalidatedBy_UpdatePrice_8158D76B6E0BFEAA70B5CB758CE36A82 = new MethodInfo[]
    {
      typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetPrice(string)' could not be found using reflection.")
    };
  }
  public PriceUpdater(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}