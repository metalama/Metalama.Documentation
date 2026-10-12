// This is public domain Metalama sample code.

using Doc.LAMA0703.Error;

// Error: the aspect is applied to the assembly, which isn't a type.
[assembly: Audit]

namespace Doc.LAMA0703.Error;

public interface IAuditSink
{
    void Write( string message );
}

public partial class OrderService
{
    public void PlaceOrder() { }
}
