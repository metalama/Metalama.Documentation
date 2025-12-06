using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace Doc.ValueAdapter;
public sealed class ProductCatalogue
{
  private readonly Dictionary<string, decimal> _dbSimulator = new()
  {
    ["corn"] = 100
  };
  public int DbOperationCount { get; private set; }
  // Very weird API but suppose it's legacy and we need to keep it, but cache it.
  [Cache]
  public StringBuilder GetProductsAsStringBuilder()
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetProductsAsStringBuilder_Source();
    }
    return _cachingService.GetFromCacheOrExecute<StringBuilder>(_cacheRegistration_GetProductsAsStringBuilder, this, new object[] { }, Invoke);
  }
  private StringBuilder GetProductsAsStringBuilder_Source()
  {
    Console.WriteLine("Getting the product list from database.");
    this.DbOperationCount++;
    var stringBuilder = new StringBuilder();
    foreach (var productId in this._dbSimulator.Keys)
    {
      if (stringBuilder.Length > 0)
      {
        stringBuilder.Append(",");
      }
      stringBuilder.Append(productId);
    }
    return stringBuilder;
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetProductsAsStringBuilder;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetProductsAsStringBuilder = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProductsAsStringBuilder", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetProductsAsStringBuilder()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}