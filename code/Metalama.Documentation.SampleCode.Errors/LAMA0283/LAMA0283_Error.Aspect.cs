// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0283.Error;

// Compile-time classes, including aspects, can't have a primary constructor.
public class LogAttribute( string category ) : OverrideMethodAspect
{
    private readonly string _category = category;

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this._category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
