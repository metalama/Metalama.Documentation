// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0282.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // A regular string literal is compatible with C# 10.
        meta.InsertComment( "Logged by the \"Log\" aspect." );
        Console.WriteLine( $"Entering {meta.Target.Method}." );

        return meta.Proceed();
    }
}
