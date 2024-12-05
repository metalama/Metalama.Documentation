// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.HashingKeyBuilder;

public sealed class FileSystem
{
    public int OperationCount { get; private set; }

    [Cache]
    public byte[] ReadAll( string path )
    {
        this.OperationCount++;

        Console.WriteLine( "Reading the whole file." );

        return new byte[100 + this.OperationCount];
    }
}