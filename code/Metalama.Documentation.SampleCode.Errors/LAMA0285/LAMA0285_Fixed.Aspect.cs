// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0285.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        this.LogEntry();

        return meta.Proceed();
    }

    // Fixed: the auxiliary template is a method of the aspect class.
    [Template]
    private void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method.Name}." );
    }
}
