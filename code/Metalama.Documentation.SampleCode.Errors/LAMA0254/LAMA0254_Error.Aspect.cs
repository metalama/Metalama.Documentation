// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Threading.Tasks;

namespace Doc.LAMA0254.Error;

public class LogAttribute : OverrideMethodAspect
{
    public bool ContinueOnCapturedContext { get; set; }

    public override dynamic? OverrideMethod() => meta.Proceed();

    public override async Task<dynamic?> OverrideAsyncMethod()
    {
        Console.WriteLine( $"Starting {meta.Target.Method.Name}." );

        // Error: the argument of ConfigureAwait must be the literal true or false.
        var result = await meta.ProceedAsync().ConfigureAwait( this.ContinueOnCapturedContext );

        return result;
    }
}
