using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.Reflection;
namespace Doc.RedisConfiguration;
public sealed class CloudCalculator
{
  public int OperationCount { get; private set; }
  [Cache]
  public int Add(int a, int b)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((CloudCalculator)instance).Add_Source((int)args[0], (int)args[1]);
    }
    return _cachingService.GetFromCacheOrExecute<int>(_cacheRegistration_Add, this, new object[] { a, b }, Invoke);
  }
  private int Add_Source(int a, int b)
  {
    Console.WriteLine("Doing some very hard work.");
    this.OperationCount++;
    return a + b;
  }
  private static readonly CachedMethodMetadata _cacheRegistration_Add;
  private ICachingService _cachingService;
  static CloudCalculator()
  {
    _cacheRegistration_Add = CachedMethodMetadata.Register(typeof(CloudCalculator).GetMethod("Add", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(int), typeof(int) }, null) ?? throw new MissingMethodException("The method 'CloudCalculator.Add(int, int)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
  public CloudCalculator(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}