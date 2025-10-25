using Metalama.Framework.RunTime;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.ExcludeThisParameter;
[CachingConfiguration(IgnoreThisParameter = true)]
public class PricingService
{
  private readonly Guid _id = Guid.NewGuid();
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
  [Cache]
  public string[] GetProducts(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PricingService)instance).GetProducts_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts, this, new object[] { productId }, Invoke);
  }
  private string[] GetProducts_Source(string productId) => throw new NotImplementedException();
  public override string ToString() => $"CurrencyService {this._id}";
  private static readonly CachedMethodMetadata _cacheRegistration_GetProductPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static PricingService()
  {
    _cacheRegistration_GetProductPrice = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProductPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("PricingService.GetProductPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = true, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(PricingService).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("PricingService.GetProducts(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = true, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public PricingService([AspectGenerated] ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}