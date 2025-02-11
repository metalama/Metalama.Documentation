// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;
using Metalama.Patterns.Contracts;

// ReSharper disable StringLiteralTypo

namespace Doc.Localize;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SetOptions( new ContractOptions { Templates = new FrenchTemplates() } );
    }
}