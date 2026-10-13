// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Diagnostics;

namespace Doc.LAMA0021.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}

public class MeasureAttribute : OverrideMethodAspect
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
            Console.WriteLine( $"{meta.Target.Method} took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
}
