// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0889.Fixed;

[CompileTime]
internal static class LogSettings
{
    // Fixed: the field is read-only, so its value can't change between compilations.
    public static readonly string Prefix = "Entering";
}

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{LogSettings.Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
