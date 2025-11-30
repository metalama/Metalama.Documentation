// This is public domain Metalama sample code.

using Metalama.Extensions.Metrics;
using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Metrics;

namespace Doc.LogComplexMethods;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Add logging to methods with more than 50 syntax nodes.
        amender
            .SelectMany( p => p.Types )
            .SelectMany( t => t.Methods )
            .Where( m => m.Metrics().Get<SyntaxNodesCount>().Value > 50 )
            .AddAspectIfEligible<LogAttribute>();
    }
}
