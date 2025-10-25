using Metalama.Framework.RunTime;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.AbsoluteExpiration_Attribute;
[CachingConfiguration(AbsoluteExpiration = 60)]
public class PricingService
{
  [Cache]
  public decimal GetProductPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PricingService)instance).GetProductPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetProductPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetProductPrice_Source(string productId) => throw new NotImplementedException();
  [Cache(AbsoluteExpiration = 20)]
  public string[] GetProducts(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PricingService)instance).GetProducts_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts, this, new object[] { productId }, Invoke);
  }
  private string[] GetProducts_Source(string productId) => throw new NotImplementedException();
  private static readonly CachedMethodMetadata _cacheRegistration_GetProductPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static PricingService()
  {
    _cacheRegistration_GetProductPrice = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProductPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("PricingService.GetProductPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = new TimeSpan(36000000000L), AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("PricingService.GetProducts(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = new TimeSpan(12000000000L), AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public PricingService([AspectGenerated] ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}