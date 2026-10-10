// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0280.Error;

[CompileTime]
internal class ArgumentStatistics
{
    public int NullCount { get; set; }
}

public class LogNullArgumentsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var statistics = new ArgumentStatistics();

        foreach ( var parameter in meta.Target.Parameters )
        {
            if ( parameter.Value == null )
            {
                // Error: a property of a compile-time object is incremented under a run-time condition.
                statistics.NullCount++;
            }
        }

        Console.WriteLine( $"{meta.Target.Method.Name} received {statistics.NullCount} null argument(s)." );

        return meta.Proceed();
    }
}
