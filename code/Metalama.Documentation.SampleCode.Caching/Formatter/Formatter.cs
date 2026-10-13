// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.IO;

namespace Doc.Formatter;

public sealed class FileSystem
{
    public int OperationCount { get; private set; }

    [Cache]
    public byte[] ReadAll( FileInfo file )
    {
        this.OperationCount++;

        Console.WriteLine( "Reading the whole file." );

        return new byte[100 + this.OperationCount];
    }
}