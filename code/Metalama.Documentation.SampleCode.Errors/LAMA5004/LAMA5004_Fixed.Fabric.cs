// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Framework.Options;
using Metalama.Patterns.Contracts;

namespace Doc.LAMA5004.Fixed;

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Enables the [SuspendInvariants] aspect and the SuspendInvariants method.
        amender.SetOptions( new ContractOptions { IsInvariantSuspensionSupported = true } );
    }
}
