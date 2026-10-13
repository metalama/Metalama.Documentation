// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace Doc.CompanyPolicy;

// A shared package of the company. Its transitive fabric executes in every project that
// references the package.
public class HttpTimingPolicy : TransitiveProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(HttpClient) )
                .Methods( nameof(HttpClient.GetStringAsync) )
                .RedirectToSynthesizedMethod( nameof(this.Time) ) );

    [Template]
    public dynamic? Time()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine( $"HTTP call took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
}
