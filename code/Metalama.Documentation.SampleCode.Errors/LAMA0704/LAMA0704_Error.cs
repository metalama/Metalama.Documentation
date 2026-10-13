// This is public domain Metalama sample code.

namespace Doc.LAMA0704.Error;

// The dependency type is internal.
internal interface IAuditSink
{
    void Write( string message );
}

// Error: the public constructor of OrderService can't receive an internal IAuditSink.
[Audit]
public partial class OrderService
{
    public void PlaceOrder() { }
}
