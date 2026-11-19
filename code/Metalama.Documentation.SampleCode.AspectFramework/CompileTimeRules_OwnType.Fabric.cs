// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Collections.Immutable;
using System.Linq;

namespace Doc.CompileTimeRules_OwnType;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        var policy = new LogPolicy( "Accounts", ImmutableArray.Create( "password" ) );

        amender
            .SelectMany( p => p.Types )
            .Where( t => t.Name.EndsWith( "Service" ) )
            .SelectMany( t => t.Methods )
            .Where( m => m.Accessibility == Accessibility.Public )
            .AddAspect( _ => new LogAspect( policy ) );
    }
}
