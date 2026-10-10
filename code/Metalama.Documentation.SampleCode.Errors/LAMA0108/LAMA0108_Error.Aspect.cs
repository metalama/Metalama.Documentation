// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0108.Error;

public class LogNullArgumentsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var nullArgumentCount = meta.CompileTime( 0 );

        foreach ( var parameter in meta.Target.Parameters )
        {
            if ( parameter.Value == null )
            {
                // Error: a compile-time variable is incremented under a run-time condition.
                nullArgumentCount++;
            }
        }

        Console.WriteLine( $"{meta.Target.Method.Name} received {nullArgumentCount} null argument(s)." );

        return meta.Proceed();
    }
}
