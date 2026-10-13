// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0283.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    private readonly string _category;

    // The primary constructor is replaced by an explicit constructor.
    public LogAttribute( string category )
    {
        this._category = category;
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this._category}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
