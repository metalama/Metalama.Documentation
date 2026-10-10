// This is public domain Metalama sample code.

namespace Doc.LAMA0703.Fixed;

public interface IAuditSink
{
    void Write( string message );
}

// Fixed: the aspect is applied to a type.
[Audit]
public partial class OrderService
{
    public void PlaceOrder() { }
}
