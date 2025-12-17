using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Doc.ImperativeInvalidate;
public sealed partial class ProductCatalogue
{
  private readonly Dictionary<string, decimal> _dbSimulator = new()
  {
    ["corn"] = 100
  };
  public int DbOperationCount { get; private set; }
  // [<snippet Cache>]
  [Cache]
  public decimal GetPrice(string productId)
  {
    // [<endsnippet Cache>]
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetPrice_Source(string productId)
  // [<endsnippet Cache>]
  {
    Console.WriteLine($"Getting the price of {productId} from database.");
    this.DbOperationCount++;
    return this._dbSimulator[productId];
  }
  [Cache]
  public string[] GetProducts()
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetProducts_Source();
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts, this, new object[] { }, Invoke);
  }
  private string[] GetProducts_Source()
  {
    Console.WriteLine("Getting the product list from database.");
    this.DbOperationCount++;
    return this._dbSimulator.Keys.ToArray();
  }
  public void AddProduct(string productId, decimal price)
  {
    Console.WriteLine($"Adding the product {productId}.");
    this.DbOperationCount++;
    this._dbSimulator.Add(productId, price);
    this._cachingService.Invalidate(this.GetProducts);
  }
  public void UpdatePrice(string productId, decimal price)
  {
    if (!this._dbSimulator.ContainsKey(productId))
    {
      throw new KeyNotFoundException();
    }
    Console.WriteLine($"Updating the price of {productId}.");
    this.DbOperationCount++;
    this._dbSimulator[productId] = price;
    // [<snippet InvalidateCache>]
    this._cachingService.Invalidate(this.GetPrice, productId);
  // [<endsnippet InvalidateCache>]
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("ProductCatalogue.GetPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}