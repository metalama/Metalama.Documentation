// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.RetainedDeclaration_Good;

public class Fabric : ProjectFabric
{
    // A durable reference holds only a string identifier. Declaring the field as IDurableRef<INamedType>
    // also makes the conversion mandatory at every assignment, so a later edit cannot omit it.
    private IDurableRef<INamedType>? _registry;

    public override void AmendProject( IProjectAmender amender )
    {
        if ( TypeFactory.TryGetType( "Doc.Model.EntityRegistry", out var registry ) )
        {
            this._registry = registry.ToDurableRef();
        }
    }

    // Resolve against the compilation you are working on, never against a stored one.
    private INamedType? GetRegistry( ICompilation compilation )
        => this._registry?.GetTarget( compilation );
}
