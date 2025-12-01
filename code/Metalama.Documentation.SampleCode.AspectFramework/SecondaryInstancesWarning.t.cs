// Warning MY001 on `GetOrderStatus`: `The [Log] aspect is applied 2 times to the same method. Only the primary instance will be used.`
using System;
namespace Doc.SecondaryInstancesWarning;
public class OrderService
{
  // This method only has [Log] from the fabric - no warning.
  public void PlaceOrder(string productId, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder");
    try
    {
      Console.WriteLine("Placing order.");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving OrderService.PlaceOrder");
    }
  }
  // This method has [Log] from both the attribute and the fabric.
  // A warning will be reported about the duplicate.
  [Log]
  public void GetOrderStatus(string orderId)
  {
    Console.WriteLine("Entering OrderService.GetOrderStatus");
    try
    {
      Console.WriteLine("Getting order status.");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving OrderService.GetOrderStatus");
    }
  }
}
