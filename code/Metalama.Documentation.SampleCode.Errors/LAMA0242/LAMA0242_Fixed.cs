// This is public domain Metalama sample code.

namespace Doc.LAMA0242.Fixed;

public class Person
{
    public string Name { get; set; } = "";
}

public class Customer : Person
{
    public int LoyaltyPoints { get; set; }
}

internal class OrderService
{
    [LogCustomer]
    public void PlaceOrder( Person buyer, int quantity ) { }
}
