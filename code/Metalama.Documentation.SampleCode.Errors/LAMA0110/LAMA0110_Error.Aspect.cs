// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0110.Error;

public class LogNullResultAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var result = meta.Proceed();

        if ( result == null )
        {
            var i = meta.CompileTime( 0 );

            // Error: a compile-time while loop is used under a run-time condition.
            while ( i < meta.Target.Parameters.Count )
            {
                Console.WriteLine( $"{meta.Target.Method.Name} returned null for {meta.Target.Parameters[i].Name} = {meta.Target.Parameters[i].Value}." );
                i++;
            }
        }

        return result;
    }
}
