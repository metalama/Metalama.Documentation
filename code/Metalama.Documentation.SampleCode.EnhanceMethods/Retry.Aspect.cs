// This is public domain Metalama sample code.

using System;
using System.Threading;
using Metalama.Framework.Aspects;

namespace Doc.RetryFew;

public class RetryAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        for ( var i = 0;; i++ )
        {
            try
            {
                return meta.Proceed();
            }
            catch ( Exception e ) when ( i < 3 )
            {
                Console.WriteLine( $"{e.Message}. Retrying in 100 ms." );
                Thread.Sleep( 100 );
            }
        }
    }
}