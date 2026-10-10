// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0247.Fixed;

public class RetryOnFailureAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // The local function returns bool, so the target method must return bool.
        bool Execute()
        {
            return meta.Proceed();
        }

        var succeeded = Execute();

        if ( !succeeded )
        {
            Console.WriteLine( "Retrying once." );
            succeeded = Execute();
        }

        return succeeded;
    }
}
