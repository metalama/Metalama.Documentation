// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0270.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the Log template is called as a stand-alone statement.
        this.Log();

        return meta.Proceed();
    }

    [Template]
    private void Log()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );
    }
}
