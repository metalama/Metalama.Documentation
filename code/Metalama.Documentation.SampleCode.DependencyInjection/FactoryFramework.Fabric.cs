// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace Doc.FactoryFramework;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            builder => builder.RegisterFramework<FactoryDependencyInjectionFramework>() );
    }
}
