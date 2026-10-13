// This is public domain Metalama sample code.

namespace Doc.LAMA0704.Fixed;

// Fixed: the dependency type is public, like the constructor that receives it.
public interface IAuditSink
{
    void Write( string message );
}

[Audit]
public partial class OrderService
{
    public void PlaceOrder() { }
}
