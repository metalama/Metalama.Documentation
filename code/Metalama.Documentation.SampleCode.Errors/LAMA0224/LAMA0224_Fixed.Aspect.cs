// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0224.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        try
        {
            // Fixed: the variable is initialized in its declaration.
            var result = meta.Proceed();

            Console.WriteLine( $"{meta.Target.Method.Name} returned {result}." );

            return result;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Method.Name} failed: {e.Message}" );

            throw;
        }
    }
}
