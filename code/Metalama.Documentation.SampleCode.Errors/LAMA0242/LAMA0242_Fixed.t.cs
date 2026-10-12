using System;
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
  public void PlaceOrder(Person buyer, int quantity)
  {
    var customer = buyer as Customer;
    if (customer != null)
    {
      Console.WriteLine($"Customer {customer.Name} has {customer.LoyaltyPoints} loyalty points.");
    }
  }
}