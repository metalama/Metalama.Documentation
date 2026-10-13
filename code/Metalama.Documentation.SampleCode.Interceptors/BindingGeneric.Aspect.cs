// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.BindingGeneric;

public class TraceCacheAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var interceptor = typeof(CacheTracing).AsINamedType()
            .Methods.OfName( nameof(CacheTracing.Get) )
            .Single();

        builder.InterceptMethods(
            calls => calls
                .Type( typeof(Cache) )
                .Methods( nameof(Cache.Get) )
                .RedirectToExistingMethod( interceptor ) );
    }
}

public static class CacheTracing
{
    // The type parameter T is bound by name to the type parameter T of Cache.Get.
    // [<snippet Interceptor>]
    public static T? Get<T>( string key )
    // [<endsnippet Interceptor>]
    {
        Console.WriteLine( $"Reading {key} as {typeof(T).Name}." );

        return Cache.Get<T>( key );
    }
}
