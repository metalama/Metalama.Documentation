// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.PassCallerInstance;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.InterceptMethods(
            calls => calls
                .Type( typeof(Telemetry) )
                .Methods( nameof(Telemetry.Track) )
                .ForEachSite(
                    site => site
                        .RedirectToExistingMethod(
                            typeof(TelemetryWithSender).AsINamedType().Methods.OfName( nameof(TelemetryWithSender.Track) ).Single() )
                        // [<snippet This>]
                        .WithArgs( new { sender = site.Caller.CanAccessThis ? ExpressionFactory.This() : null } ) ) );
                        // [<endsnippet This>]
}
