// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0246.Fixed;

public class LogResultAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the parameter has a specific type.
        void PrintResult( object? value )
        {
            Console.WriteLine( $"Returning {value}." );
        }

        var result = meta.Proceed();
        PrintResult( result );

        return result;
    }
}
