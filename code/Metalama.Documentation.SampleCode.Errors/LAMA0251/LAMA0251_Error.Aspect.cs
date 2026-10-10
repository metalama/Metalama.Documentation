// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0251.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: 'methodName' is dynamic, but the method name is a compile-time string.
        dynamic methodName = meta.Target.Method.Name;
        Console.WriteLine( $"Entering {methodName}." );

        return meta.Proceed();
    }
}
