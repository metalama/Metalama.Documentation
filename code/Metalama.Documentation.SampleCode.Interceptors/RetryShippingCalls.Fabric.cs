// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.RetryShippingCalls;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(ShippingClient) )
                .AllMethods()
                // [<snippet Registration>]
                .Where( m => m.Name.StartsWith( "Get" ) )
                .RedirectToSynthesizedMethod( nameof(this.Retry) )
                .WithArgs( new { maxAttempts = 3 } ) );
                // [<endsnippet Registration>]

    [Template]
    // [<snippet Retry>]
    public dynamic? Retry( [CompileTime] int maxAttempts )
    {
        for ( var attempt = 1;; attempt++ )
        {
            try
            {
                return meta.Proceed();
            }
            catch ( TimeoutException e ) when ( attempt < maxAttempts )
            {
                Console.WriteLine( $"Attempt {attempt} failed: {e.Message} Retrying." );
            }
        }
    }
    // [<endsnippet Retry>]
}
