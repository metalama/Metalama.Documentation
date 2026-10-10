// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0225.Fixed;

public class ReturnDefaultOnExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: meta.Default returns the default value of the return type as a dynamic expression.
        dynamic? result = meta.Default( meta.Target.Method.ReturnType );

        try
        {
            result = meta.Proceed();
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Method.Name} failed: {e.Message}" );
        }

        return result;
    }
}
