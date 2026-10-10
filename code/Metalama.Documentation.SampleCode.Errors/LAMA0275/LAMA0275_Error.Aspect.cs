// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0275.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the call to the virtual LogEntry template omits the optional argument.
        LogEntry();

        return meta.Proceed();
    }

    // Derived aspects can override this template to customize the log message.
    [Template]
    protected virtual void LogEntry( [CompileTime] string verb = "Executing" )
    {
        Console.WriteLine( $"{verb} {meta.Target.Method}." );
    }
}
