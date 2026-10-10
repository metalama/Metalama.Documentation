// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0104.Error;

public class LogFirstParameterAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: 'index' is a run-time variable, but it is used to index a compile-time collection.
        var index = 0;
        Console.WriteLine( $"First parameter: {meta.Target.Parameters[index].Name}" );

        return meta.Proceed();
    }
}
