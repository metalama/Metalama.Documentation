// This is public domain Metalama sample code.

using System;

namespace Doc.FakeSleep;

internal class Program
{
    private static void Main()
    {
        for ( var attempt = 1; attempt <= 3; attempt++ )
        {
            Backoff.WaitBeforeAttempt( attempt );
        }

        Console.WriteLine( $"Virtual time elapsed: {VirtualClock.Elapsed.TotalSeconds} s." );
    }
}
