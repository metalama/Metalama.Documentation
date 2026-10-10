// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.LAMA5005.Error;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureContracts( new ContractOptions { IsInvariantSuspensionSupported = true } );
    }
}
