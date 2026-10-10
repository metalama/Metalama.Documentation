using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Doc.LAMA5102.Fixed;
public sealed class ProductCatalogue
{
  private readonly Dictionary<string, decimal> _prices = new()
  {
    ["corn"] = 100
  };
  [Cache]
  public decimal GetPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetPrice_Source(string productId) => this._prices[productId];
  // Fixed: the parameter has the same name as in GetPrice.
  [InvalidateCache(nameof(GetPrice))]
  public void UpdatePrice(string productId, decimal price)
  {
    this._prices[productId] = price;
    object result = null;
    _cachingService.Invalidate(_methodsInvalidatedBy_UpdatePrice_356CCE52C3A0531D2B3AADAFC8C24F36[0], this, new object[] { productId });
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  private ICachingService _cachingService;
  private static MethodInfo[] _methodsInvalidatedBy_UpdatePrice_356CCE52C3A0531D2B3AADAFC8C24F36;
  static ProductCatalogue()
  {
    _methodsInvalidatedBy_UpdatePrice_356CCE52C3A0531D2B3AADAFC8C24F36 = new MethodInfo[]
    {
      typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetPrice(string)' could not be found using reflection.")
    };
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetPrice(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}