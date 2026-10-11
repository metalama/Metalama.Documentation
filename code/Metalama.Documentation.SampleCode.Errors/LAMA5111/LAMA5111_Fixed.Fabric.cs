// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;

namespace Doc.LAMA5111.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
        => amender.ConfigureCaching( caching => caching.AddParameterClassifier(
                                         "Stream",
                                         new StreamParameterClassifier() ) );
}
