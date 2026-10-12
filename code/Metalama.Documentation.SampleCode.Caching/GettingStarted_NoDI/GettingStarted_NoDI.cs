// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.GettingStarted_NoDI;

public static class CloudCalculator
{
    public static int OperationCount { get; private set; }

    [Cache]
    public static int Add( int a, int b )
    {
        Console.WriteLine( "Doing some very hard work." );

        OperationCount++;

        return a + b;
    }
}