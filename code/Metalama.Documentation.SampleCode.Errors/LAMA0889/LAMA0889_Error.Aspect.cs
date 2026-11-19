// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0889.Error;

[CompileTime]
internal static class LogSettings
{
    // Warning: a static field of a compile-time type must be read-only.
    public static string Prefix = "Entering";
}

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{LogSettings.Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
