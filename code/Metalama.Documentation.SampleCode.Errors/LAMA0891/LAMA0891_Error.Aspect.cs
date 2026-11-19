// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0891.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Warning: the field also exists at compile time, where it is shared by all compilations.
    public static string Prefix = "Entering";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
