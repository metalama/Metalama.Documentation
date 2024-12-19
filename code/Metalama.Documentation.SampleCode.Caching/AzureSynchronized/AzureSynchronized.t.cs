using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System.Reflection;
namespace Doc.AzureSynchronized;
public record Product(string Id, decimal Price, string? Remarks = null);
public sealed partial class ProductCatalogue
{
  // This instance is intentionally shared between both app instances to simulate
  // a shared database.
  private static readonly ConcurrentDictionary<string, Product> _dbSimulator = new()
  {
    ["corn"] = new Product("corn", 100, "Initial record.")
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
    Console.WriteLine($"Getting the product of {productId} from database.");
    this.DbOperationCount++;
    return _dbSimulator[productId];
  }
  public void Update(Product product)
  {
    if (!_dbSimulator.ContainsKey(product.Id))
    {
      throw new KeyNotFoundException();
    }
    Console.WriteLine($"Updating the product {product.Id}.");
    this.DbOperationCount++;
    _dbSimulator[product.Id] = product;
    this._cachingService.Invalidate(this.GetProduct, product.Id);
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetProduct;
  private ICachingService _cachingService;
  static ProductCatalogue()
  {
    _cacheRegistration_GetProduct = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProduct", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("ProductCatalogue.GetProduct(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}