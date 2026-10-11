// This is public domain Metalama sample code.

namespace Doc.LAMA1017.Error;

[InterceptProcess]
public class OrderService
{
    private sealed class Order { }

    private static void Process( Order order ) { }

    public static void Run() => Process( new Order() );
}
