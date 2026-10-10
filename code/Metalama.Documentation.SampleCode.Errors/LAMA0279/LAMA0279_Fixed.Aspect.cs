// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0279.Fixed;

public abstract class LogAttributeBase : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        this.LogEntry();

        return meta.Proceed();
    }

    // Fixed: the template is virtual and has an empty default implementation.
    [Template]
    protected virtual void LogEntry() { }
}

public class ConsoleLogAttribute : LogAttributeBase
{
    protected override void LogEntry()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );
    }
}
