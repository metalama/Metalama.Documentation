// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0114.Fixed;

public class TraceAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Use meta.This only when the target method is not static.
        if ( meta.Target.Method.IsStatic )
        {
            Console.WriteLine( $"Executing {meta.Target.Method.Name}." );
        }
        else
        {
            Console.WriteLine( $"Executing {meta.Target.Method.Name} on {meta.This}." );
        }

        return meta.Proceed();
    }
}
