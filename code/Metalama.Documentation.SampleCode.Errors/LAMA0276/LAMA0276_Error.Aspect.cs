// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0276.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        LogResultType<int>();

        return meta.Proceed();
    }

    // Error: the type parameter T of this called template is run-time.
    [Template]
    private void LogResultType<T>()
    {
        Console.WriteLine( $"{meta.Target.Method.Name} returns {typeof( T ).Name}." );
    }
}
