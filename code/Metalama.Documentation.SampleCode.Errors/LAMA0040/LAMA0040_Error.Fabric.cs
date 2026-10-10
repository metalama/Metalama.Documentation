// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0040.Error;

internal class Fabric : ProjectFabric
{
    private readonly string _typeName;

    // Error: Metalama can't instantiate a fabric whose constructor has parameters.
    public Fabric( string typeName )
    {
        this._typeName = typeName;
    }

    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectTypes()
            .Where( t => t.Name == this._typeName )
            .SelectMany( t => t.Methods )
            .AddAspect<LogAttribute>();
    }
}
