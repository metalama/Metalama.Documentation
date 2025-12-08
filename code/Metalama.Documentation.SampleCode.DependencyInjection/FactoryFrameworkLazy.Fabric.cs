// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace Doc.FactoryFrameworkLazy;

internal class LazyFabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            builder => builder.RegisterFramework<FactoryDependencyInjectionFramework>() );
    }
}
