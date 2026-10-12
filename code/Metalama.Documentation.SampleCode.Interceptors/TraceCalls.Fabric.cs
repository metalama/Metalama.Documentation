// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Collections.Generic;

namespace Doc.TraceCalls;

// Delete this file to remove the tracing.
internal class TracingFabric : ProjectFabric
{
    // [<snippet Registration>]
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(PricingClient) )
                .AllMethods()
                .RedirectToSynthesizedMethod( nameof(TracingTemplates.Trace) )
                .WithTemplateProvider( TemplateProvider.FromInstance( new TracingTemplates() ) )
                .WithGranularity( InterceptorGranularity.Member ) );
    // [<endsnippet Registration>]
}

internal class TracingTemplates : ITemplateProvider
{
    [Template]
    public dynamic? Trace()
    {
        // [<snippet Arguments>]
        var arguments = new List<string>();

        foreach ( var parameter in meta.Interception.Method.ArgumentParameters )
        {
            arguments.Add( $"{parameter.Name}: {parameter.Value}" );
        }

        var call = $"{meta.Interception.CallingMember.Name} -> "
                   + $"{meta.Interception.Method.InterceptedMethod.Name}({string.Join( ", ", arguments )})";
        // [<endsnippet Arguments>]

        try
        {
            var result = meta.Proceed();
            Console.WriteLine( $"{call} returned {result}." );

            return result;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"{call} threw {e.GetType().Name}: {e.Message}" );

            throw;
        }
    }
}
