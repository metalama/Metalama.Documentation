// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0224.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: a variable of type dynamic must be initialized in its declaration.
        dynamic? result;

        try
        {
            result = meta.Proceed();
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Method.Name} failed: {e.Message}" );

            throw;
        }

        Console.WriteLine( $"{meta.Target.Method.Name} returned {result}." );

        return result;
    }
}
