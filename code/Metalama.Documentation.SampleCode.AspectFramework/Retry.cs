// This is public domain Metalama sample code.

using System;

namespace Doc.Retry;

internal class Foo
{
    [Retry]
    private void RetryDefault()
    {
        throw new InvalidOperationException();
    }

    [Retry( MaxAttempts = 10 )]
    private void RetryTenTimes()
    {
        throw new InvalidOperationException();
    }
}