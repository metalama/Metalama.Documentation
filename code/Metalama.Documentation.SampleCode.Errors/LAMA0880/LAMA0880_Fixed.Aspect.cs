// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0880.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: the field is read-only, so only the constructor can assign it.
    public readonly string Category;

    public LogAttribute( string category )
    {
        this.Category = category;
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
