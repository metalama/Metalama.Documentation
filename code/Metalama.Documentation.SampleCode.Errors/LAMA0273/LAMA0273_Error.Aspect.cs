// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0273.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the LogEntry template is assigned to a delegate instead of being invoked.
        var logEntry = LogEntry;
        logEntry();

        return meta.Proceed();
    }

    [Template]
    private void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );
    }
}
