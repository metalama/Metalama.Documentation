// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.DisablePreconditions;

public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        // Disable preconditions for the entire DisabledService class.
        amender
            .SelectReflectionType( typeof(DisabledService) )
            .ConfigureContracts( new ContractOptions { ArePreconditionsEnabled = false } );

        // Disable preconditions for a specific method.
        amender
            .SelectReflectionType( typeof(MixedService) )
            .SelectMany( t => t.Methods.OfName( "UnvalidatedMethod" ) )
            .ConfigureContracts( new ContractOptions { ArePreconditionsEnabled = false } );
    }
}
