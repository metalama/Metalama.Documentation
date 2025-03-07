using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using Metalama.Patterns.Caching.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Doc.ObjectDependencies;
internal static class GlobalDependencies
{
  public static ICacheDependency ProductCatalogue = new StringDependency(nameof(ProductCatalogue));
  public static ICacheDependency ProductList = new StringDependency(nameof(ProductList));
}
public record Product(string Name, decimal Price) : ICacheDependency
{
  string ICacheDependency.GetCacheKey(ICachingService cachingService)
  {
    if (cachingService == null !)
    {
      throw new ArgumentNullException("cachingService", "The 'cachingService' parameter must not be null.");
    }
    return this.Name;
  }
  // Means that when we invalidate the current product in cache, we should also invalidate the product catalogue.
  IReadOnlyCollection<ICacheDependency> ICacheDependency.CascadeDependencies { get; } = new[]
  {
    GlobalDependencies.ProductCatalogue
  };
}
public sealed partial class ProductCatalogue
{
  private readonly Dictionary<string, Product> _dbSimulator = new()
  {
    ["corn"] = new Product("corn", 100)
  };
  public int DbOperationCount { get; private set; }
  [Cache]
  public Product GetProduct(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetProduct_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<Product>(_cacheRegistration_GetProduct, this, new object[] { productId }, Invoke);
  }
  private Product GetProduct_Source(string productId)
  {
    Console.WriteLine($"Getting the price of {productId} from database.");
    this.DbOperationCount++;
    var product = this._dbSimulator[productId];
    // [<snippet AddDependency>]
    this._cachingService.AddDependency(product);
    // [<endsnippet AddDependency>]
    return product;
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
    this._cachingService.AddDependency(GlobalDependencies.ProductList);
    return this._dbSimulator.Keys.ToArray();
  }
  [Cache]
  public IReadOnlyCollection<Product> GetPriceList()
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetPriceList_Source();
    }
    return _cachingService.GetFromCacheOrExecute<IReadOnlyCollection<Product>>(_cacheRegistration_GetPriceList, this, new object[] { }, Invoke);
  }
  private IReadOnlyCollection<Product> GetPriceList_Source()
  {
    this.DbOperationCount++;
    this._cachingService.AddDependency(GlobalDependencies.ProductCatalogue);
    return this._dbSimulator.Values;
  }
  public void AddProduct(Product product)
  {
    Console.WriteLine($"Adding the product {product.Name}.");
    this.DbOperationCount++;
    this._dbSimulator.Add(product.Name, product);
    this._cachingService.Invalidate(product);
    this._cachingService.Invalidate(GlobalDependencies.ProductList);
  }
  public void UpdateProduct(Product product)
  {
    if (!this._dbSimulator.ContainsKey(product.Name))
    {
      throw new KeyNotFoundException();
    }
    Console.WriteLine($"Updating the price of {product.Name}.");
    this.DbOperationCount++;
    this._dbSimulator[product.Name] = product;
    // [<snippet Invalidate>]
    this._cachingService.Invalidate(product);
  // [<endsnippet Invalidate>]
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetPriceList;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProduct;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetProduct = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProduct", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("ProductCatalogue.GetProduct(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
    _cacheRegistration_GetPriceList = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetPriceList", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null).ThrowIfMissing("ProductCatalogue.GetPriceList()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}