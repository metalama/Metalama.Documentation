// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace Doc.LAMA0038.Fixed;

internal class ServiceBase
{
    // Fixed: the aspect is added directly to the method of the base class.
    [Log]
    public void Ping() { }
}

internal class OrderService : ServiceBase
{
    public void PlaceOrder( int productId ) { }

    private class Fabric : TypeFabric
    {
        public override void AmendType( ITypeAmender amender )
        {
            // Fixed: the type fabric adds aspects only to the members of its own type.
            amender.SelectMany( t => t.Methods ).AddAspect<LogAttribute>();
        }
    }
}
