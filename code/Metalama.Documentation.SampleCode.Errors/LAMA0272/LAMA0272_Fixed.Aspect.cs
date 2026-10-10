// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0272.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        this.Log();

        return meta.Proceed();
    }

    [Template]
    private void Log()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        // Fixed: the redundant return statement is removed.
    }
}
