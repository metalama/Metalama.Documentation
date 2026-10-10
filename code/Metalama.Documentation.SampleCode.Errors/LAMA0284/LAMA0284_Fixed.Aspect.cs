// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0284.Fixed;

public class LogReturnValueAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // meta.RunTime makes the lambda block run-time.
        var log = meta.RunTime( new Action<object?>( value => { Console.WriteLine( value?.ToString() ); } ) );

        var result = meta.Proceed();
        log( result );

        return result;
    }
}
