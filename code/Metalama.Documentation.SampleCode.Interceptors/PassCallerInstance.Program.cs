// This is public domain Metalama sample code.

namespace Doc.PassCallerInstance;

internal class Program
{
    private static void Main()
    {
        new OrderService().PlaceOrder( "Laptop" );
        OrderService.Reset();
    }
}
