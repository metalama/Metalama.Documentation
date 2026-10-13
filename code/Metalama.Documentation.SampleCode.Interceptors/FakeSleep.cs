// This is public domain Metalama sample code.

using System.Threading;

namespace Doc.FakeSleep;

public static class Backoff
{
    public static void WaitBeforeAttempt( int attempt ) => Thread.Sleep( 1000 * attempt );
}
