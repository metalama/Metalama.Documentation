// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0246.Error;

public class LogResultAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the parameter of a local function cannot be dynamic.
        void PrintResult( dynamic? value )
        {
            Console.WriteLine( $"Returning {value}." );
        }

        var result = meta.Proceed();
        PrintResult( result );

        return result;
    }
}
