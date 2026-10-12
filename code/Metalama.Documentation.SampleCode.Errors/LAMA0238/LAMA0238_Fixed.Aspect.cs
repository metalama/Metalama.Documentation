// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0238.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the field has a concrete type, so its value is known at compile time.
    private readonly string _category = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this._category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
