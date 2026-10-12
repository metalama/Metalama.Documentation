// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.DedupPerMember;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(LoggingTemplates.Log) )
                .WithTemplateProvider( TemplateProvider.FromInstance( new LoggingTemplates() ) )
                // [<snippet Granularity>]
                .WithGranularity( InterceptorGranularity.Member ) );
                // [<endsnippet Granularity>]
}

internal class LoggingTemplates : ITemplateProvider
{
    // The template reads the calling member, so each calling member gets its own generated method.
    [Template]
    public dynamic? Log()
    {
        // [<snippet CallingMember>]
        Console.WriteLine( $"{meta.Interception.CallingMember.Name} calls {meta.Interception.Method.InterceptedMethod.Name}." );
        // [<endsnippet CallingMember>]

        return meta.Proceed();
    }
}
