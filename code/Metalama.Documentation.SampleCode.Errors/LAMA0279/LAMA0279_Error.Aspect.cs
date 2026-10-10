// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0279.Error;

public abstract class LogAttributeBase : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: LogEntry is an abstract template, so it can't be called.
        this.LogEntry();

        return meta.Proceed();
    }

    [Template]
    protected abstract void LogEntry();
}

public class ConsoleLogAttribute : LogAttributeBase
{
    protected override void LogEntry()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );
    }
}
