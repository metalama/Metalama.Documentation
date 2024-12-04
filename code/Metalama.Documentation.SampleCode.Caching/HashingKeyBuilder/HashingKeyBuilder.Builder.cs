// This is public domain Metalama sample code.

using Flashtrace.Formatters;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Formatters;
using System;
using System.Collections.Generic;
using System.IO.Hashing;

namespace Doc.HashingKeyBuilder;

internal sealed class HashingKeyBuilder : ICacheKeyBuilder, IDisposable
{
    private readonly CacheKeyBuilder _underlyingBuilder;

    public HashingKeyBuilder( IFormatterRepository formatters )
    {
        this._underlyingBuilder = new CacheKeyBuilder(
            formatters,
            new CacheKeyBuilderOptions() { MaxKeySize = 8000 } );
    }

    public string BuildMethodKey(
        CachedMethodMetadata metadata,
        object? instance,
        IList<object?> arguments )
    {
        var fullKey = this._underlyingBuilder.BuildMethodKey( metadata, instance, arguments );

        return Hash( fullKey );
    }

    public string BuildDependencyKey( object o )
    {
        var fullKey = this._underlyingBuilder.BuildDependencyKey( o );

        return Hash( fullKey );
    }

    private static string Hash( string s )
    {
        unsafe
        {
            fixed ( byte* hashBytes = stackalloc byte[128] )
            fixed ( char* input = s )
            {
                var span = new ReadOnlySpan<byte>( input, s.Length * 2 );
                var hashSpan = new Span<byte>( hashBytes, 128 );
                XxHash128.Hash( span, hashSpan );

                return Convert.ToBase64String( hashSpan );
            }
        }
    }

    public void Dispose()
    {
        this._underlyingBuilder.Dispose();
    }
}