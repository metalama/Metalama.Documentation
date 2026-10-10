// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0528.Error;

[Disposable]
internal partial class FileLogger
{
    // Error: this method is private because it has no access modifier, so it can't implement IDisposable.Dispose.
    void Dispose()
    {
        Console.WriteLine( "Closing the log file." );
    }
}
