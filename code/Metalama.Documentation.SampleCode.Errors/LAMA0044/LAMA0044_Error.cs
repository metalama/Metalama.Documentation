// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.LAMA0044.Error;

internal class OrderService
{
    public void PlaceOrder( [NotNull] string customerId ) { }

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // Error: a type fabric cannot set options for its containing namespace.
            amender.Select( t => t.ContainingNamespace )
                .ConfigureContracts( new ContractOptions { IsInheritable = false } );
        }
    }
}
