// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.GettingStarted;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Leaving {meta.Target.Method}" );
        }
    }
}