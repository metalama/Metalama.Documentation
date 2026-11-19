// This is public domain Metalama sample code.

namespace Doc.CompileTimeRules_Immutable;

public class OrderService
{
    [Log( Category = "Orders" )]
    public void PlaceOrder( string product, int quantity ) { }
}
