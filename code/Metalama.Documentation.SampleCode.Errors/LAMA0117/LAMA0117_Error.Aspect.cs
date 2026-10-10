// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0117.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // This helper is neither a template nor marked [CompileTime], so it is run-time-or-compile-time.
    public static bool IsMethod( object declaration )
    {
        // Error: IMethod is compile-time-only.
        return declaration is IMethod;
    }
}
