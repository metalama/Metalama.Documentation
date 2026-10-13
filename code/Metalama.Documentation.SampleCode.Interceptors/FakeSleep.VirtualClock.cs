// This is public domain Metalama sample code.

using System;

namespace Doc.FakeSleep;

public static class VirtualClock
{
    public static TimeSpan Elapsed { get; private set; }

    public static void Sleep( int millisecondsTimeout )
        => Elapsed += TimeSpan.FromMilliseconds( millisecondsTimeout );
}
