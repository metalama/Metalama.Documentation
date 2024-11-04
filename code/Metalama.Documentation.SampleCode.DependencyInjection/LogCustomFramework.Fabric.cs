// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace Doc.LogCustomFramework;
public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            dependencyInjection
                => dependencyInjection.RegisterFramework<LoggerDependencyInjectionFramework>() );
    }
}