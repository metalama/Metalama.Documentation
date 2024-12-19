using Flashtrace.Formatters;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using Metalama.Patterns.Caching.Formatters;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Doc.CacheKeyAspect;
public abstract class Entity : IFormattable<CacheKeyFormatting>
{
  protected Entity(string kind, int id)
  {
    this.Kind = kind;
    this.Id = id;
  }
  [CacheKey]
  public string Kind { get; }
  [CacheKey]
  public int Id { get; }
  public string? Description { get; set; }
  void IFormattable<CacheKeyFormatting>.Format(UnsafeStringBuilder stringBuilder, IFormatterRepository formatterRepository)
  {
    this.FormatCacheKey(stringBuilder, formatterRepository);
  }
  protected virtual void FormatCacheKey(UnsafeStringBuilder stringBuilder, IFormatterRepository formatterRepository)
  {
    stringBuilder.Append(GetType().FullName);
    if (formatterRepository.Role is CacheKeyFormatting)
    {
      stringBuilder.Append(" ");
      formatterRepository.Get<int>().Format(stringBuilder, Id);
      stringBuilder.Append(" ");
      formatterRepository.Get<string>().Format(stringBuilder, Kind);
    }
  }
}
public class EntityService
{
  [Cache]
  public IEnumerable<Entity> GetRelatedEntities(Entity entity)
  {
    static object? Invoke(object? instance, object? [] args)
    {
      return ((EntityService)instance).GetRelatedEntities_Source((Entity)args[0]);
    }
    return _cachingService.GetFromCacheOrExecute<IEnumerable<Entity>>(_cacheRegistration_GetRelatedEntities, this, new object[] { entity }, Invoke);
  }
  private IEnumerable<Entity> GetRelatedEntities_Source(Entity entity) => throw new NotImplementedException();
  private static readonly CachedMethodMetadata _cacheRegistration_GetRelatedEntities;
  private ICachingService _cachingService;
  static EntityService()
  {
    _cacheRegistration_GetRelatedEntities = CachedMethodMetadata.Register(typeof(EntityService).GetMethod("GetRelatedEntities", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(Entity) }, null).ThrowIfMissing("EntityService.GetRelatedEntities(Entity)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
  }
  public EntityService(ICachingService? cachingService = null)
  {
    this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
  }
}