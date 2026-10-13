// This is public domain Metalama sample code.

namespace Doc.BeforeTypeConstructor;

internal class Program
{
    private static void Main()
    {
        // Touching each closed generic type triggers its static
        // constructor, which registers it with the router.
        _ = new Handler<OrderPlaced>();
        _ = new Handler<OrderShipped>();

        MessageRouter.Dispatch( new OrderPlaced( "O-42" ) );
        MessageRouter.Dispatch( new OrderShipped( "O-42" ) );
    }
}
