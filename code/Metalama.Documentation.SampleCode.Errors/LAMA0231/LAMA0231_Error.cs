// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0231.Error;

internal class OrderService
{
    public void PlaceOrder( string product, int quantity ) { }

    public void CancelOrder( int orderId ) { }

    // Error: only a type fabric can be nested in a run-time class.
    private class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.SelectTypes()
                .Where( t => t.Name == "OrderService" )
                .SelectMany( t => t.Methods )
                .AddAspect<LogAttribute>();
        }
    }
}
