// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.CompileTimeForEach;

internal class CompileTimeForEachAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        foreach ( var p in meta.Target.Parameters.Where( p => p.RefKind != RefKind.Out ) )
        {
            Console.WriteLine( $"{p.Name} = {p.Value}" );
        }

        return meta.Proceed();
    }
}