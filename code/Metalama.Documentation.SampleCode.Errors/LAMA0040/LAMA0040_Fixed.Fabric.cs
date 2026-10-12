// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0040.Fixed;

internal class Fabric : ProjectFabric
{
    // Fixed: the fabric has no constructor parameter, so Metalama can instantiate it.
    private const string TypeName = "OrderService";

    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectTypes()
            .Where( t => t.Name == TypeName )
            .SelectMany( t => t.Methods )
            .AddAspect<LogAttribute>();
    }
}
