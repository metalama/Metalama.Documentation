// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.ProjectFabric_;

public class Log : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"Exiting {meta.Target.Method}." );
        }
    }
}