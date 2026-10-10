// This is public domain Metalama sample code.

using System;

namespace Doc.LAMA0529.Fixed;

internal class LoggerBase : IDisposable
{
    // Fixed: the method is virtual, so the aspect can override it in FileLogger.
    public virtual void Dispose()
    {
        Console.WriteLine( "Releasing the logger." );
    }
}

[Disposable]
internal partial class FileLogger : LoggerBase { }
