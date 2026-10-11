using Metalama.Framework.RunTime;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using System;
using System.IO;
using System.Reflection;
namespace Doc.LAMA5111.Fixed;
public class DocumentService
{
  // Fixed: the method receives the file path, which can be part of the cache key.
  [Cache]
  public int CountLines(string path)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((DocumentService)instance).CountLines_Source((string)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<int>(_cacheRegistration_CountLines, this, new object[] { path }, Invoke);
  }
  private int CountLines_Source(string path)
  {
    return File.ReadAllLines(path).Length;
  }
  private static readonly CachedMethodMetadata _cacheRegistration_CountLines;
  private ICachingService _cachingService;
  static DocumentService()
  {
    _cacheRegistration_CountLines = CachedMethodMetadata.Register(typeof(DocumentService).GetMethod("CountLines", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null) ?? throw new MissingMethodException("The method 'DocumentService.CountLines(string)' could not be found using reflection."), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, false);
  }
  public DocumentService([AspectGenerated] ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}