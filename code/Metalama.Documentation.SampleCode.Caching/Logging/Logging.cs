// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @DisableCompareProgramOutput -- random IDs
#endif
using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.Logging;

public sealed class CloudCalculator
{
    public int OperationCount { get; private set; }

    [Cache]
    public int Add( int a, int b )
    {
        Console.WriteLine( "Doing some very hard work." );

        this.OperationCount++;

        return a + b;
    }
}