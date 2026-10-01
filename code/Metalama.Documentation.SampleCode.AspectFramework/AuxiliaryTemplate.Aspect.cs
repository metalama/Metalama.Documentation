// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Concurrent;

namespace Doc.AuxiliaryTemplate;

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

        if ( this._cache.TryGetValue( cacheKey, out var returnValue ) )
        {
            this.LogCacheHit( cacheKey, returnValue );
        }
        else
        {
            this.LogCacheMiss( cacheKey );
            returnValue = meta.Proceed();
            this._cache.TryAdd( cacheKey, returnValue );
        }

        return returnValue;
    }

    // This method is an auxiliary template.

    [Template]
    protected virtual void LogCacheHit( string cacheKey, object? value ) { }

    // This method is an auxiliary template.
    [Template]
    protected virtual void LogCacheMiss( string cacheKey ) { }
}

internal class CacheAndLogAttribute : CacheAttribute
{
    protected override void LogCacheHit( string cacheKey, object? value )
    {
        Console.WriteLine( $"Cache hit: {cacheKey} => {value}" );
    }

    protected override void LogCacheMiss( string cacheKey )
    {
        Console.WriteLine( $"Cache hit: {cacheKey}" );
    }
}