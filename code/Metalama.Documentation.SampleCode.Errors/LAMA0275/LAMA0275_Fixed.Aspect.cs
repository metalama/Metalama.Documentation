// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0275.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the call to the virtual LogEntry template specifies all arguments.
        LogEntry( "Executing" );

        return meta.Proceed();
    }

    // Derived aspects can override this template to customize the log message.
    [Template]
    protected virtual void LogEntry( [CompileTime] string verb = "Executing" )
    {
        Console.WriteLine( $"{verb} {meta.Target.Method}." );
    }
}
