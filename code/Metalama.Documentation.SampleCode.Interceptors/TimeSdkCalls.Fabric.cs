// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Doc.TimeSdkCalls;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                // [<snippet Selector>]
                .RedirectToSynthesizedMethod(
                    new MethodTemplateSelector(
                        nameof(TimingTemplates.Time),
                        nameof(TimingTemplates.TimeAsync),
                        useAsyncTemplateForAnyAwaitable: true ) )
                // [<endsnippet Selector>]
                .WithTemplateProvider( TemplateProvider.FromInstance( new TimingTemplates() ) ) );
}

internal class TimingTemplates : ITemplateProvider
{
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
            Console.WriteLine( $"{meta.Interception.Method.InterceptedMethod.Name} took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
    // [<endsnippet Template>]

    [Template]
    // [<snippet AsyncTemplate>]
    public async Task<dynamic?> TimeAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await meta.ProceedAsync();
        }
        finally
        {
            Console.WriteLine( $"{meta.Interception.Method.InterceptedMethod.Name} took {stopwatch.ElapsedMilliseconds} ms." );
        }
    }
    // [<endsnippet AsyncTemplate>]
}
