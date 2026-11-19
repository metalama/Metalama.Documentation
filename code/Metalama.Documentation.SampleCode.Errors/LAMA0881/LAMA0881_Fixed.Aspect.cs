// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0881.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the init accessor accepts the named argument but no later assignment.
    public string Category { get; init; } = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
