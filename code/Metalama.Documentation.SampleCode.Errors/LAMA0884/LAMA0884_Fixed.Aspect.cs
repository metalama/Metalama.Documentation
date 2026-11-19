// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Immutable;

namespace Doc.LAMA0884.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    // Fixed: ImmutableArray<string> is a concrete immutable type.
    private readonly ImmutableArray<string> _categories;

    public LogAttribute( params string[] categories )
    {
        this._categories = ImmutableArray.Create( categories );
    }

    public override dynamic? OverrideMethod()
    {
        var categories = string.Join( ", ", this._categories );
        Console.WriteLine( $"[{categories}] Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
