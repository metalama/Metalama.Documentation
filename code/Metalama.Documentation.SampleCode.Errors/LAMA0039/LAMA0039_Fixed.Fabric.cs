// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0039.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Fixed: AddAspectIfEligible skips the methods that aren't eligible for [Log].
        amender.SelectTypes()
            .Where( t => t.Name == "Calculator" )
            .SelectMany( t => t.Methods )
            .AddAspectIfEligible<LogAttribute>();
    }
}
