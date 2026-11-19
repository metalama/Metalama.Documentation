// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Utilities;
using System;

namespace Doc.LAMA0886.Error;

// Warning: every aspect is already bound by the immutability contract, so the attribute is redundant.
[ImmutableType]
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
