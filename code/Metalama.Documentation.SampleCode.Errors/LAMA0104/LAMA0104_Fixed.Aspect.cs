// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0104.Fixed;

public class LogFirstParameterAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: meta.CompileTime makes 'index' a compile-time variable.
        var index = meta.CompileTime( 0 );
        Console.WriteLine( $"First parameter: {meta.Target.Parameters[index].Name}" );

        return meta.Proceed();
    }
}
