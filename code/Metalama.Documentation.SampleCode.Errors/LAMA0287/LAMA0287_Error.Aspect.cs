// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0287.Error;

public class TraceAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // meta.Receiver requires an instance member or an extension method.
        Console.WriteLine( $"Executing {meta.Target.Method.Name} on {meta.Receiver}." );

        return meta.Proceed();
    }
}
