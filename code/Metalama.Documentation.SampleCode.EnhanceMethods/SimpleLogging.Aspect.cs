// This is public domain Metalama sample code.

using System;
using Metalama.Framework.Aspects;

namespace Doc.SimpleLog;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Enhancing the method.
        Console.WriteLine( $"Simply logging a method..." );

        // Let the method do its own thing.
        return meta.Proceed();
    }
}