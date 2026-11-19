// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0886.Fixed;

// Fixed: the attribute is removed. The aspect is still checked because it implements IAspect.
public class LogAttribute : OverrideMethodAspect
{
    public LogAttribute( string category )
    {
        this.Category = category;
    }

    public string Category { get; }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
