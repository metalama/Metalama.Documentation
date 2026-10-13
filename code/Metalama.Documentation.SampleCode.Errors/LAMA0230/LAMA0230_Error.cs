// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0230.Error;

internal class OrderService
{
    public void PlaceOrder( string product, int quantity ) { }

    public void CancelOrder( int orderId ) { }

    // Error: a type fabric nested in a run-time class must be private.
    public class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            amender.SelectMany( t => t.Methods ).AddAspect<LogAttribute>();
        }
    }
}
