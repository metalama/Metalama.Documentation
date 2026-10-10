// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0210.Fixed;

public class RetryAttribute : OverrideMethodAspect
{
    public RetryAttribute( int maxAttempts )
    {
        this.Policy = new RetryPolicy( maxAttempts );
    }

    public RetryPolicy Policy { get; }

    public override dynamic? OverrideMethod()
    {
        for ( var attempt = 1;; attempt++ )
        {
            try
            {
                return meta.Proceed();
            }
            catch ( Exception ) when ( attempt < this.Policy.MaxAttempts )
            {
                Console.WriteLine( $"Attempt {attempt} failed. Retrying." );
            }
        }
    }
}
