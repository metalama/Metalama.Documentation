// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Implementation;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0701.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            builder =>
            {
                // Both built-in frameworks are unregistered, and no other framework is registered.
                builder.UnregisterFramework<LoggerDependencyInjectionFramework>();
                builder.UnregisterFramework<DefaultDependencyInjectionFramework>();
            } );
    }
}
