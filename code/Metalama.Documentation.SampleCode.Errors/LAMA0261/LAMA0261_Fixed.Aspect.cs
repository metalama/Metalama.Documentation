// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0261.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the override inherits the [Template] attribute of the base method.
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
