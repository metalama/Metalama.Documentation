// This is public domain Metalama sample code.

using Metalama.Patterns.Memoization;
using System;
using System.IO.Hashing;

namespace Doc.Memoize_;

public class HashedBuffer
{
    public HashedBuffer( ReadOnlyMemory<byte> buffer )
    {
        this.Buffer = buffer;
    }

    public ReadOnlyMemory<byte> Buffer { get; }

    [Memoize]
    public ReadOnlyMemory<byte> Hash => XxHash64.Hash( this.Buffer.Span );

    [Memoize]
    public override string ToString() => $"{{HashedBuffer ({this.Buffer.Length} bytes)}}";
}