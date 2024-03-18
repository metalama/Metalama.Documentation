using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Doc.InvalidateAspect
{
  public sealed class ProductCatalogue
  {
    private readonly Dictionary<string, decimal> _dbSimulator = new()
    {
      ["corn"] = 100
    };
    public int DbOperationCount { get; private set; }
    [Cache]
    public string[] GetProducts()
    {
      static object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetProducts_Source();
      }
      return _cachingService!.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts!, this, new object[] { }, Invoke);
    }
    private string[] GetProducts_Source()
    {
      Console.WriteLine("Getting the product list from database.");
      this.DbOperationCount++;
      return this._dbSimulator.Keys.ToArray();
    }
    [Cache] /*<Cache>*/
    public decimal GetPrice(string productId) /*</Cache>*/
    {
      static object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
      }
      return _cachingService!.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice!, this, new object[] { productId }, Invoke);
    }
    private decimal GetPrice_Source(string productId)
    {
      Console.WriteLine($"Getting the price of {productId} from database.");
      this.DbOperationCount++;
      return this._dbSimulator[productId];
    }
    [InvalidateCache(nameof(GetProducts))]
    public void AddProduct(string productId, decimal price)
    {
      Console.WriteLine($"Adding the product {productId}.");
      this.DbOperationCount++;
      this._dbSimulator.Add(productId, price);
      object result = null;
      CachingServiceExtensions.Invalidate(this._cachingService!, ProductCatalogue._methodsInvalidatedBy_AddProduct_976614B12F3F447F4082EAE1C88C1EE0![0], this, new object[] { });
      return;
    }
    [InvalidateCache(nameof(GetPrice))] /*<InvalidateCache>*/
    public void UpdatePrice(string productId, decimal price) /*</InvalidateCache>*/
    {
      if (!this._dbSimulator.ContainsKey(productId))
      {
        throw new KeyNotFoundException();
      }
      Console.WriteLine($"Updating the price of {productId}.");
      this.DbOperationCount++;
      this._dbSimulator[productId] = price;
      object result = null;
      CachingServiceExtensions.Invalidate(this._cachingService!, ProductCatalogue._methodsInvalidatedBy_UpdatePrice_DA3C5EB2E8FE3C0C2B256E589481CF14![0], this, new object[] { productId });
      return;
    }
    private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
    private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
    private ICachingService _cachingService;
    private static MethodInfo[] _methodsInvalidatedBy_AddProduct_976614B12F3F447F4082EAE1C88C1EE0;
    private static MethodInfo[] _methodsInvalidatedBy_UpdatePrice_DA3C5EB2E8FE3C0C2B256E589481CF14;
    static ProductCatalogue()
    {
      ProductCatalogue._cacheRegistration_GetProducts = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)!, "ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
      ProductCatalogue._cacheRegistration_GetPrice = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "ProductCatalogue.GetPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
      ProductCatalogue._methodsInvalidatedBy_AddProduct_976614B12F3F447F4082EAE1C88C1EE0 = new MethodInfo[]
      {
        RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)!, "ProductCatalogue.GetProducts()")
      };
      ProductCatalogue._methodsInvalidatedBy_UpdatePrice_DA3C5EB2E8FE3C0C2B256E589481CF14 = new MethodInfo[]
      {
        RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "ProductCatalogue.GetPrice(string)")
      };
    }
    public ProductCatalogue(ICachingService? cachingService = default)
    {
      this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
    }
  }
}