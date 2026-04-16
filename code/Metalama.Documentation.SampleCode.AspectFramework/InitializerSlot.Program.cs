// This is public domain Metalama sample code.

namespace Doc.InitializerSlot;

internal class Program
{
    private static void Main()
    {
        _ = new Order { OrderId = "o-1", CustomerId = "alice" };

        _ = new SubscriptionOrder
        {
            OrderId = "o-2",
            CustomerId = "bob",
            RenewalIntervalDays = 30
        };
    }
}
