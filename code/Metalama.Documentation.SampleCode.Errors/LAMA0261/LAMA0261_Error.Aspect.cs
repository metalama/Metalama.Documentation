// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0261.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Error: the base method is already a template, so [Template] is redundant.
    [Template]
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
