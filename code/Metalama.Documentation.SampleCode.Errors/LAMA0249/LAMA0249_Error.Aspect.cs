// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0249.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Error: a template cannot be unsafe.
    public override unsafe dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
