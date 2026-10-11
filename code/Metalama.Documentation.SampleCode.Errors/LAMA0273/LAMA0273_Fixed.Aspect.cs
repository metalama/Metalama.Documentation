// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0273.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the LogEntry template is invoked directly.
        LogEntry();

        return meta.Proceed();
    }

    [Template]
    private void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );
    }
}
