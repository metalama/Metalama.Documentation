// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0039.Error;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Error: AddAspect also selects the static method, which isn't eligible for [Log].
        amender.SelectTypes()
            .Where( t => t.Name == "Calculator" )
            .SelectMany( t => t.Methods )
            .AddAspect<LogAttribute>();
    }
}
