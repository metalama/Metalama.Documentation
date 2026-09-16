// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Diagnostics;

namespace Doc.ProjectFabric_TwoAspects;

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

public class Profile : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine(
                $"{meta.Target.Method} completed in {stopwatch.ElapsedMilliseconds}." );
        }
    }
}