// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0230.Fixed;

internal class OrderService
{
    public void PlaceOrder( string product, int quantity ) { }

    public void CancelOrder( int orderId ) { }

    // Fixed: the type fabric is private.
    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            amender.SelectMany( t => t.Methods ).AddAspect<LogAttribute>();
        }
    }
}
