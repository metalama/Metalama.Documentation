// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.RetainedDeclaration_Wrong;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        if ( TypeFactory.TryGetType( "Doc.Model.EntityRegistry", out var registry ) )
        {
            amender
                .SelectTypes()
                .Where( t => t.Name.EndsWith( "Entity", StringComparison.Ordinal ) )
                .AddAspect( _ => new RegisterEntityAspect( registry ) );
        }
    }
}

internal sealed class RegisterEntityAspect : IAspect<INamedType>
{
    // WRONG. An INamedType belongs to the compilation it came from, and the aspect outlives every
    // compilation, so this field pins one whole version of the project for as long as the solution is open.
    private readonly INamedType _registry;

    public RegisterEntityAspect( INamedType registry )
    {
        this._registry = registry;
    }

    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Use this._registry here.
    }
}
