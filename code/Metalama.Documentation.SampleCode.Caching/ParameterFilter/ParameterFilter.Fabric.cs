// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;

namespace Doc.ParameterFilter;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.ConfigureCaching( caching => caching.AddParameterClassifier(
                                         "ILogger",
                                         new LoggerParameterClassifier() ) );
}