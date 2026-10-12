// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0293.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        try
        {
            // For an async iterator, this code becomes 'yield return' statements inside the try block.
            var result = meta.Proceed();

            return result;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Method} failed: {e.Message}" );

            throw;
        }
    }
}
