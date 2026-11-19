// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.RetainedDeclaration_Good;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        if ( TypeFactory.TryGetType( "Doc.Model.EntityRegistry", out var registry ) )
        {
            // Convert the declaration once. The lambda then captures only the durable reference.
            var registryRef = registry.ToDurableRef();

            amender
                .SelectTypes()
                .Where( t => t.Name.EndsWith( "Entity", StringComparison.Ordinal ) )
                .AddAspect( _ => new RegisterEntityAspect( registryRef ) );
        }
    }
}

internal sealed class RegisterEntityAspect : IAspect<INamedType>
{
    // A durable reference holds only a string identifier. Declaring the field as IDurableRef<INamedType>
    // also makes the conversion mandatory at every assignment, so a later edit cannot omit it.
    private readonly IDurableRef<INamedType> _registry;

    public RegisterEntityAspect( IDurableRef<INamedType> registry )
    {
        this._registry = registry;
    }

    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Resolve against the compilation you are working on, never against a stored one.
        var registry = this._registry.GetTarget( builder.Target.Compilation );
    }
}
