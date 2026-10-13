// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0287.Fixed;

public class TraceAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Use meta.Receiver only when the target method has a receiver.
        if ( meta.Target.Method.HasReceiver() )
        {
            Console.WriteLine( $"Executing {meta.Target.Method.Name} on {meta.Receiver}." );
        }
        else
        {
            Console.WriteLine( $"Executing {meta.Target.Method.Name}." );
        }

        return meta.Proceed();
    }
}
