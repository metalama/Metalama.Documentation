// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0114.Error;

public class TraceAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // meta.This requires an instance method.
        Console.WriteLine( $"Executing {meta.Target.Method.Name} on {meta.This}." );

        return meta.Proceed();
    }
}
