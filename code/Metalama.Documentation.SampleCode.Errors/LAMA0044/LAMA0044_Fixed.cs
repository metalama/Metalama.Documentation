// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.LAMA0044.Fixed;

internal class OrderService
{
    public void PlaceOrder( [NotNull] string customerId ) { }

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // Fixed: the type fabric sets options only for its own type.
            amender.ConfigureContracts( new ContractOptions { IsInheritable = false } );
        }
    }
}
