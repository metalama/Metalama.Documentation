// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.LAMA0884.Error;

public class LogAttribute : OverrideMethodAspect
{
    // Warning: the object behind IReadOnlyList<string> may still be modified.
    private readonly IReadOnlyList<string> _categories;

    public LogAttribute( params string[] categories )
    {
        this._categories = categories;
    }

    public override dynamic? OverrideMethod()
    {
        var categories = string.Join( ", ", this._categories );
        Console.WriteLine( $"[{categories}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
