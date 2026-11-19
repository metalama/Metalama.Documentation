// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0891.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the field is read-only and of an immutable type.
    public static readonly string Prefix = "Entering";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
