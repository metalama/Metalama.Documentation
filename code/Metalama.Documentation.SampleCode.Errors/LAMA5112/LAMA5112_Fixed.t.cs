using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Framework.RunTime;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.Linq;
using System.Reflection;
namespace Doc.LAMA5112.Fixed;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public partial class PriceCatalog
{
  [Cache]
  public decimal GetPrice(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PriceCatalog)instance).GetPrice_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice, this, new object[] { productId }, Invoke);
  }
  private decimal GetPrice_Source(string productId) => productId.Length * 10m;
  public decimal GetStock(string productId)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((PriceCatalog)instance).GetStock_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetStock, this, new object[] { productId }, Invoke);
  }
  private decimal GetStock_Source(string productId) => productId.Length * 3m;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private sealed class Fabric : TypeFabric
  {
    public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
  private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
  private static readonly CachedMethodMetadata _cacheRegistration_GetStock;
  private ICachingService _cachingService;
  static PriceCatalog()
  {
    _cacheRegistration_GetStock = CachedMethodMetadata.Register(typeof(PriceCatalog).GetMethod("GetStock", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'PriceCatalog.GetStock(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
    _cacheRegistration_GetPrice = CachedMethodMetadata.Register(typeof(PriceCatalog).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'PriceCatalog.GetPrice(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
  public PriceCatalog([AspectGenerated] ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}