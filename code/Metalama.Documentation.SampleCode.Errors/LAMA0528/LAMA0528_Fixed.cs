// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0528.Fixed;

[Disposable]
internal partial class FileLogger
{
    // Fixed: the method is public, so the aspect can override it to implement IDisposable.Dispose.
    public void Dispose()
    {
        Console.WriteLine( "Closing the log file." );
    }
}
