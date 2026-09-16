// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.Logging;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var targetMethodName = meta.Target.Method.ToDisplayString();

        try
        {
            Console.WriteLine( $"Started {targetMethodName}" );

            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Finished {targetMethodName}" );
        }
    }
}