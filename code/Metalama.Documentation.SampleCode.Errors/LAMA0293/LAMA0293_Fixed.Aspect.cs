// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.LAMA0293.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        try
        {
            var result = meta.Proceed();

            return result;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{meta.Target.Method} failed: {e.Message}" );

            throw;
        }
    }

    // Fixed: async iterators use a dedicated template without a try-catch block around 'yield return'.
    public override async IAsyncEnumerable<dynamic?> OverrideAsyncEnumerableMethod()
    {
        Console.WriteLine( $"Enumerating {meta.Target.Method}." );

        await foreach ( var item in meta.ProceedAsyncEnumerable() )
        {
            yield return item;
        }

        Console.WriteLine( $"{meta.Target.Method} completed." );
    }
}
