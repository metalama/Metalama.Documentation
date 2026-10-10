// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0223.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Calling {meta.Target.Method.Name}." );

        // Fixed: meta.Proceed() works with any return type.
        return meta.Proceed();
    }
}
