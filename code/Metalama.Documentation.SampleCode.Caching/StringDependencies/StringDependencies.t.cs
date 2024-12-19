using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
namespace Doc.StringDependencies;
public sealed partial class ProductCatalogue
{
  private readonly Dictionary<string, decimal> _dbSimulator = new()
  {
    ["corn"] = 100
  };
  public int DbOperationCount { get; private set; }
  [Cache]
  public decimal GetPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetPrice_Source(string productId)
  {
    Console.WriteLine($"Getting the price of {productId} from database.");
    this.DbOperationCount++;
    // [<snippet AddDependency>]
    this._cachingService.AddDependency($"ProductPrice:{productId}");
    // [<endsnippet AddDependency>]
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
    this._cachingService.AddDependency("ProductList");
    return this._dbSimulator.Keys.ToArray();
  }
  [Cache]
  public ImmutableDictionary<string, decimal> GetPriceList()
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPriceList_Source();
    }
    return _cachingService.GetFromCacheOrExecute<ImmutableDictionary<string, decimal>>(_cacheRegistration_GetPriceList, this, new object[] { }, Invoke);
  }
  private ImmutableDictionary<string, decimal> GetPriceList_Source()
  {
    this.DbOperationCount++;
    this._cachingService.AddDependency("PriceList");
    return this._dbSimulator.ToImmutableDictionary();
  }
  public void AddProduct(string productId, decimal price)
  {
    Console.WriteLine($"Adding the product {productId}.");
    this.DbOperationCount++;
    this._dbSimulator.Add(productId, price);
    this._cachingService.Invalidate("ProductList", "PriceList");
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
    // [<snippet Invalidate>]
    this._cachingService.Invalidate($"ProductPrice:{productId}", "PriceList");
  // [<endsnippet Invalidate>]
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetPriceList;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("ProductCatalogue.GetPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
    _cacheRegistration_GetPriceList = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPriceList", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetPriceList()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}