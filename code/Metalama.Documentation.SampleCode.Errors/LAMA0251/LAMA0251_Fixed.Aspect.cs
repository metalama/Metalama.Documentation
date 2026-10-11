// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0251.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: 'var' makes 'methodName' a compile-time variable.
        var methodName = meta.Target.Method.Name;
        Console.WriteLine( $"Entering {methodName}." );

        return meta.Proceed();
    }
}
