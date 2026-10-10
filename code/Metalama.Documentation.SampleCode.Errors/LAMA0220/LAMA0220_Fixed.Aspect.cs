// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0220.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the reusable code is a template method, called as a stand-alone statement.
        LogEntry();

        return meta.Proceed();
    }

    [Template]
    private void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );
    }
}
