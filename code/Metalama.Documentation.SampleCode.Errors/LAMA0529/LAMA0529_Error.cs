// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0529.Error;

internal class LoggerBase : IDisposable
{
    // Error: this method isn't virtual, so the aspect can't override it in FileLogger.
    public void Dispose()
    {
        Console.WriteLine( "Releasing the logger." );
    }
}

[Disposable]
internal partial class FileLogger : LoggerBase { }
