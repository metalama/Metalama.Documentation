// This is public domain Metalama sample code.

namespace Doc.LAMA0200.Error;

[MethodOverloadCount]
public partial class OrderService
{
    public void Submit() { }

    public void Submit( int priority ) { }

    public void Cancel() { }
}
