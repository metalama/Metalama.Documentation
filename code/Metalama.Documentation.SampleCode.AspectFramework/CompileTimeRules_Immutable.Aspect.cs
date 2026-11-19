// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.CompileTimeRules_Immutable;

public sealed class LogAttribute : OverrideMethodAspect
{
    // The property is set by a named argument of the custom attribute. An init accessor
    // is enough for that, and it keeps the aspect immutable once it has been created.
    public string Category { get; init; } = "General";

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"[{this.Category}] Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
