// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Microsoft.CodeAnalysis;
using System;

namespace Doc.LAMA0291.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // This helper is neither a template nor marked [CompileTime], so it is run-time-or-compile-time.
    public static bool IsRoslynSymbol( object declaration )
    {
        // Error: ISymbol is a Roslyn type, which is compile-time-only in this project.
        return declaration is ISymbol;
    }
}
