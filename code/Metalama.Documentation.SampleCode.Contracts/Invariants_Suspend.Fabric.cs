// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;
using Metalama.Patterns.Contracts;

namespace Doc.Invariants_Suspend;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SetOptions( new ContractOptions { IsInvariantSuspensionSupported = true } );
    }
}