// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0238.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Error: a field of an aspect cannot be of type dynamic because it is not a template.
    private readonly dynamic _category = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this._category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
