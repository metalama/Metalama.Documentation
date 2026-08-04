// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.RetainedDeclaration_Good;

public class Fabric : ProjectFabric
{
    // A durable reference holds only a string identifier. Typing the field IDurableRef<INamedType> also makes
    // the conversion impossible to forget: the compiler asks for it at every assignment.
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
