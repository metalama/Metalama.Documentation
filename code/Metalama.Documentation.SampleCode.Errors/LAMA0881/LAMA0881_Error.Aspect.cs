// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0881.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Warning: the setter is public, so code outside the aspect can change the property.
    public string Category { get; set; } = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
