// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Diagnostics;

namespace Doc.TimeSdkCalls;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(this.Time) ) );

    [Template]
    // [<snippet Template>]
    public dynamic? Time()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return meta.Proceed();
        }
        finally
        {
            var methodName = meta.Interception.Method.InterceptedMethod.Name;
            Console.WriteLine( $"{methodName} took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
    // [<endsnippet Template>]
}
