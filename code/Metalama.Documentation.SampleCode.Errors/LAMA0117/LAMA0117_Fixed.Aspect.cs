// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0117.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // Fixed: the helper is marked [CompileTime], so it can use compile-time-only types.
    [CompileTime]
    public static bool IsMethod( object declaration )
    {
        return declaration is IMethod;
    }
}
