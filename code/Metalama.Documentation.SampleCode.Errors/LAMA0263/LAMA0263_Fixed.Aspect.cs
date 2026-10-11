// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0263.Fixed;

public class LogFirstArgumentAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the cast to object gives the body of the lambda a non-dynamic type.
        Func<object?> getArgument = () => (object?) meta.Target.Parameters[0].Value;
        Console.WriteLine( $"First argument: {getArgument()}" );

        return meta.Proceed();
    }
}
