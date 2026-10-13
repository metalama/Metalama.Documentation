using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Doc.LAMA5107.Fixed;
public sealed partial class ProductCatalogue
{
  // Maps product identifiers to categories.
  private readonly Dictionary<string, string> _categories = new()
  {
    ["corn"] = "cereals"
  };
  [Cache]
  public string[] GetProducts()
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetProducts_Source();
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts, this, new object[] { }, Invoke);
  }
  private string[] GetProducts_Source() => this._categories.Keys.ToArray();
  [Cache]
  public string[] GetProducts(string category)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((ProductCatalogue)instance).GetProducts_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts2, this, new object[] { category }, Invoke);
  }
  private string[] GetProducts_Source(string category) => this._categories.Where(p => p.Value == category).Select(p => p.Key).ToArray();
  // Fixed: the aspect explicitly invalidates all overloads of GetProducts.
  [InvalidateCache(nameof(GetProducts), AllowMultipleOverloads = true)]
  public void AddProduct(string productId, string category)
  {
    this._categories.Add(productId, category);
    object result = null;
    _cachingService.Invalidate(_methodsInvalidatedBy_AddProduct_7D5E6CC336575F72EE1795F60AC63FA8[0], this, new object[] { });
    _cachingService.Invalidate(_methodsInvalidatedBy_AddProduct_7D5E6CC336575F72EE1795F60AC63FA8[1], this, new object[] { category });
  }
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
  private static readonly CachedMethodMetadata _cacheRegistration_GetProducts2;
  private ICachingService _cachingService;
  private static MethodInfo[] _methodsInvalidatedBy_AddProduct_7D5E6CC336575F72EE1795F60AC63FA8;
  static ProductCatalogue()
  {
    _methodsInvalidatedBy_AddProduct_7D5E6CC336575F72EE1795F60AC63FA8 = new MethodInfo[]
    {
      typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetProducts()' could not be found using reflection."),
      typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetProducts(string)' could not be found using reflection.")
    };
    _cacheRegistration_GetProducts2 = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetProducts(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
    _cacheRegistration_GetProducts = CachedMethodMetadata.Register(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) ?? throw new MissingMethodException("The method 'ProductCatalogue.GetProducts()' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public ProductCatalogue(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}