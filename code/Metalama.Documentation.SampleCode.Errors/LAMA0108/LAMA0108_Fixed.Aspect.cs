// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0108.Fixed;

public class LogNullArgumentsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the counter is a run-time variable because its value depends on run-time arguments.
        var nullArgumentCount = 0;

        foreach ( var parameter in meta.Target.Parameters )
        {
            if ( parameter.Value == null )
            {
                nullArgumentCount++;
            }
        }

        Console.WriteLine( $"{meta.Target.Method.Name} received {nullArgumentCount} null argument(s)." );

        return meta.Proceed();
    }
}
