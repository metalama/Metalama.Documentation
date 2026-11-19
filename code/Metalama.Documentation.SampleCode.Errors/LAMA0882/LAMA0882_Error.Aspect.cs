// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.LAMA0882.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Warning: List<string> is a mutable collection.
    private readonly List<string> _categories;

    public LogAttribute( params string[] categories )
    {
        this._categories = new List<string>( categories );
    }

    public override dynamic? OverrideMethod()
    {
        var categories = string.Join( ", ", this._categories );
        Console.WriteLine( $"[{categories}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
