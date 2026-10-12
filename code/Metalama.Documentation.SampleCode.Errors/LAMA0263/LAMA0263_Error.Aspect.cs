// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0263.Error;

public class LogFirstArgumentAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the body of the lambda is a dynamic expression.
        Func<object?> getArgument = () => meta.Target.Parameters[0].Value;
        Console.WriteLine( $"First argument: {getArgument()}" );

        return meta.Proceed();
    }
}
