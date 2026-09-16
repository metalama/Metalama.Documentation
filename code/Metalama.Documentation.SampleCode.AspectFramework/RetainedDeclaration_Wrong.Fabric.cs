// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.RetainedDeclaration_Wrong;

public class Fabric : ProjectFabric
{
    // WRONG. An INamedType belongs to the project snapshot it came from, and the fabric outlives every
    // snapshot, so this field pins one whole version of the project for as long as the solution is open.
    private INamedType? _registry;

    public override void AmendProject( IProjectAmender amender )
    {
        if ( TypeFactory.TryGetType( "Doc.Model.EntityRegistry", out var registry ) )
        {
            this._registry = registry;
        }
    }
}
