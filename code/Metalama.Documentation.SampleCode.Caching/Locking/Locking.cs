// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Threading;

namespace Doc.Locking;

public sealed class CloudService : IDisposable
{
    // We use barriers to make sure we wait long enough.
    private readonly Barrier _withoutLockBarrier = new( 2 );

    [Cache( ProfileName = "Locking" )]
    public byte[] ReadFileWithLock( string path )
    {
        Console.WriteLine( "Doing some very hard work." );

        Thread.Sleep( 50 );

        return new byte[32];
    }

    [Cache]
    public byte[] ReadFileWithoutLock( string path )
    {
        Console.WriteLine( "Doing some very hard work." );

        // Simulate a long-running operation.
        this._withoutLockBarrier.SignalAndWait();

        return new byte[32];
    }

    public void Dispose() => this._withoutLockBarrier.Dispose();
}