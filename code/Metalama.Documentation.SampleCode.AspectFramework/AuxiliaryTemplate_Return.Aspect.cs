// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Concurrent;

namespace Doc.AuxiliaryTemplate_Return;

internal class CacheAttribute : OverrideMethodAspect
{
    [Introduce( WhenExists = OverrideStrategy.Ignore )]
    private readonly ConcurrentDictionary<string, object?> _cache = new();

    // This method is the usual top-level template.
    public override dynamic? OverrideMethod()
    {
        // Naive implementation of a caching key.
        var cacheKey =
            $"{meta.Target.Method.Name}({string.Join( ", ", meta.Target.Method.Parameters.ToValueArray() )})";

        this.GetFromCache( cacheKey );

        var returnValue = meta.Proceed();

        this.AddToCache( cacheKey, returnValue );

        return returnValue;
    }

    // This method is an auxiliary template.

    [Template]
    protected virtual void GetFromCache( string cacheKey )
    {
        if ( this._cache.TryGetValue( cacheKey, out var returnValue ) )
        {
            meta.Return( returnValue );
        }
    }

    // This method is an auxiliary template.
    [Template]
    protected virtual void AddToCache( string cacheKey, object? returnValue )
    {
        this._cache.TryAdd( cacheKey, returnValue );
    }
}