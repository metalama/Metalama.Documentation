using Metalama.Framework.RunTime;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.ParameterFilter;
public class PricingService
{
  [Cache]
  public decimal GetProductPrice(string productId, [NotCacheKey] string? correlationId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PricingService)instance).GetProductPrice_Source((string)args[0], (string? )args[1]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetProductPrice, this, new object[] { productId, correlationId }, Invoke);
  }
  private decimal GetProductPrice_Source(string productId, string? correlationId) => throw new NotImplementedException();
  [Cache]
  public string[] GetProducts(string productId, [NotCacheKey] string? correlationId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PricingService)instance).GetProducts_Source((string)args[0], (string? )args[1]);
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts, this, new object[] { productId, correlationId }, Invoke);
  }
  private string[] GetProducts_Source(string productId, string? correlationId) => throw new NotImplementedException();
  private static readonly CachedMethodMetadata _cacheRegistration_GetProductPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static PricingService()
  {
    _cacheRegistration_GetProductPrice = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProductPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string), typeof(string) }, null).ThrowIfMissing("PricingService.GetProductPrice(string, string?)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string), typeof(string) }, null).ThrowIfMissing("PricingService.GetProducts(string, string?)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public PricingService([AspectGenerated] ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}