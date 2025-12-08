// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.ServiceLocator;
using Metalama.Framework.Fabrics;

namespace Doc.ServiceLocatorExample;

// Register the ServiceLocator framework for this test.
// Since 2026.0, ServiceLocator no longer auto-registers and must be explicitly enabled.
internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            builder => builder.RegisterFramework<ServiceLocatorDependencyInjectionFramework>() );
    }
}
