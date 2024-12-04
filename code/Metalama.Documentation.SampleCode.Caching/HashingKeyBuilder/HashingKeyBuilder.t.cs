using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.HashingKeyBuilder;
public sealed class FileSystem
{
  public int OperationCount { get; private set; }
  [Cache]
  public byte[] ReadAll(string path)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((FileSystem)instance).ReadAll_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<byte[]>(_cacheRegistration_ReadAll, this, new object[] { path }, Invoke);
  }
  private byte[] ReadAll_Source(string path)
  {
    this.OperationCount++;
    Console.WriteLine("Reading the whole file.");
    return new byte[100 + this.OperationCount];
  }
  private static readonly CachedMethodMetadata _cacheRegistration_ReadAll;
  private ICachingService _cachingService;
  static FileSystem()
  {
    _cacheRegistration_ReadAll = CachedMethodMetadata.Register(typeof(FileSystem).GetMethod("ReadAll", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null).ThrowIfMissing("FileSystem.ReadAll(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public FileSystem(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}