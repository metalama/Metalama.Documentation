// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0223.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Calling {meta.Target.Method.Name}." );

        // Error: meta.ProceedAsync() requires a method that returns a task, but GetPrice returns a decimal.
        return meta.ProceedAsync();
    }
}
