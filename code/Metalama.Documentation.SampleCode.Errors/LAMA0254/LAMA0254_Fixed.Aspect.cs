// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Threading.Tasks;

namespace Doc.LAMA0254.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public bool ContinueOnCapturedContext { get; set; }

    public override dynamic? OverrideMethod() => meta.Proceed();

    public override async Task<dynamic?> OverrideAsyncMethod()
    {
        Console.WriteLine( $"Starting {meta.Target.Method.Name}." );

        // Fixed: a compile-time if statement selects a call with a literal argument.
        if ( this.ContinueOnCapturedContext )
        {
            var result = await meta.ProceedAsync().ConfigureAwait( true );

            return result;
        }
        else
        {
            var result = await meta.ProceedAsync().ConfigureAwait( false );

            return result;
        }
    }
}
