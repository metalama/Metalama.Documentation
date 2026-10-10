// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0110.Fixed;

public class LogNullResultAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var result = meta.Proceed();

        if ( result == null )
        {
            // Fixed: a compile-time foreach loop is allowed under a run-time condition.
            foreach ( var parameter in meta.Target.Parameters )
            {
                Console.WriteLine( $"{meta.Target.Method.Name} returned null for {parameter.Name} = {parameter.Value}." );
            }
        }

        return result;
    }
}
