// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0284.Error;

public class LogReturnValueAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Metalama can't determine whether this lambda block is compile-time or run-time.
        Action<object?> log = value => { Console.WriteLine( value?.ToString() ); };

        var result = meta.Proceed();
        log( result );

        return result;
    }
}
