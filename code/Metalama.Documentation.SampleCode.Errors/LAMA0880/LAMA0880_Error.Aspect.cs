// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0880.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Warning: the field is public and not read-only, so code outside the aspect can change it.
    public string Category;

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
