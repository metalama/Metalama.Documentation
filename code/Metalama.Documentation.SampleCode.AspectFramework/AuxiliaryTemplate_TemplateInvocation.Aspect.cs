// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Concurrent;

namespace Doc.AuxiliaryTemplate_TemplateInvocation;

public class CacheAttribute : OverrideMethodAspect
{
    [Introduce( WhenExists = OverrideStrategy.Ignore )]
    private readonly ConcurrentDictionary<string, object?> _cache = new();

    public override dynamic? OverrideMethod()
    {
        this.AroundCaching( new TemplateInvocation( nameof(this.CacheOrExecuteCore) ) );

        // This should be unreachable.
        return default;
    }

    [Template]
    protected virtual void AroundCaching( TemplateInvocation templateInvocation )
    {
        meta.InvokeTemplate( templateInvocation );
    }

    [Template]
    private void CacheOrExecuteCore()
    {
        // Naive implementation of a caching key.
        var cacheKey =
            $"{meta.Target.Method.Name}({string.Join( ", ", meta.Target.Method.Parameters.ToValueArray() )})";

        if ( !this._cache.TryGetValue( cacheKey, out var returnValue ) )
        {
            returnValue = meta.Proceed();
            this._cache.TryAdd( cacheKey, returnValue );
        }

        meta.Return( returnValue );
    }
}

public class CacheAndRetryAttribute : CacheAttribute
{
    public bool IncludeRetry { get; set; }

    protected override void AroundCaching( TemplateInvocation templateInvocation )
    {
        if ( this.IncludeRetry )
        {
            for ( var i = 0;; i++ )
            {
                try
                {
                    meta.InvokeTemplate( templateInvocation );
                }
                catch ( Exception ex ) when ( i < 10 )
                {
                    Console.WriteLine( ex.ToString() );
                }
            }
        }
        else
        {
            meta.InvokeTemplate( templateInvocation );
        }
    }
}