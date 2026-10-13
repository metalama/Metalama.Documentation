// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0038.Error;

internal class ServiceBase
{
    public void Ping() { }
}

internal class OrderService : ServiceBase
{
    public void PlaceOrder( int productId ) { }

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // Error: a type fabric can't add aspects to the members of the base class.
            amender.SelectMany( t => t.BaseType!.Methods ).AddAspect<LogAttribute>();
        }
    }
}
