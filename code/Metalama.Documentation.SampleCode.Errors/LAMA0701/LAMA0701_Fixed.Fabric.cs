// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0701.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            builder =>
            {
                // Only the logger framework is unregistered. The default framework stays registered.
                builder.UnregisterFramework<LoggerDependencyInjectionFramework>();
            } );
    }
}
