// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doc.CompanyPolicy;

// A shared package of the company. Its transitive fabric executes in every project that references the package.
public class HttpTimingPolicy : TransitiveProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(HttpClient) )
                .Methods( nameof(HttpClient.GetStringAsync) )
                .RedirectToSynthesizedMethod(
                    new MethodTemplateSelector(
                        nameof(HttpTimingTemplates.Time),
                        nameof(HttpTimingTemplates.TimeAsync),
                        useAsyncTemplateForAnyAwaitable: true ) )
                .WithTemplateProvider( TemplateProvider.FromInstance( new HttpTimingTemplates() ) ) );
}

public class HttpTimingTemplates : ITemplateProvider
{
    [Template]
    public dynamic? Time() => meta.Proceed();

    [Template]
    public async Task<dynamic?> TimeAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await meta.ProceedAsync();
        }
        finally
        {
            Console.WriteLine( $"HTTP call took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
}
