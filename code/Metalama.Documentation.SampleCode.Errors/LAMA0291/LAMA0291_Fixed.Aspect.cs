// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Microsoft.CodeAnalysis;
using System;

namespace Doc.LAMA0291.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }

    // Fixed: the helper is marked [CompileTime], so it can use Roslyn types.
    [CompileTime]
    public static bool IsRoslynSymbol( object declaration )
    {
        return declaration is ISymbol;
    }
}
