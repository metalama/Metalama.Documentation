// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Concurrent;

namespace Doc.LogAndCache;

// A simple logging aspect that writes to the console.
public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Leaving {meta.Target.Method}" );
        }
    }
}

// A simple caching aspect that stores results in a dictionary.
public class CacheAttribute : OverrideMethodAspect
{
    [Introduce( WhenExists = OverrideStrategy.Ignore )]
    private static readonly ConcurrentDictionary<string, object?> _cache = new();

    public override dynamic? OverrideMethod()
    {
        // Build a cache key from method name and arguments.
        var key = $"{meta.Target.Type}.{meta.Target.Method.Name}({string.Join( ", ", meta.Target.Method.Parameters.ToValueArray() )})";

        if ( _cache.TryGetValue( key, out var cached ) )
        {
            Console.WriteLine( $"Cache hit for {key}" );

            return cached;
        }

        Console.WriteLine( $"Cache miss for {key}" );
        var result = meta.Proceed();
        _cache[key] = result;

        return result;
    }
}
