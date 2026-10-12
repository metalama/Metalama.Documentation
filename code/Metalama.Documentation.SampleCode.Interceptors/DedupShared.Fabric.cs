// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.DedupShared;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(PaymentGateway) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(LoggingTemplates.Log) )
                .WithTemplateProvider( TemplateProvider.FromInstance( new LoggingTemplates() ) ) );
}

internal class LoggingTemplates : ITemplateProvider
{
    // The template does not read the calling code, so all the call sites share one generated method.
    [Template]
    public dynamic? Log()
    {
        Console.WriteLine( $"Calling {meta.Interception.Method.InterceptedMethod.Name}." );

        return meta.Proceed();
    }
}
