// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0225.Error;

public class ReturnDefaultOnExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: null is target-typed, so it can't initialize a variable of type dynamic.
        dynamic? result = null;

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
