using System;
namespace Doc.MultipleInstances;
public class OrderService
{
  // This method has [Log] from both the custom attribute (Category="Orders")
  // and from the fabric (Category="Monitoring"). The aspect merges both categories.
  [Log(Category = "Orders")]
  public void PlaceOrder(string productId, int quantity)
  {
    Console.WriteLine("[Monitoring, Orders] Entering OrderService.PlaceOrder");
    try
    {
      Console.WriteLine("Business logic here.");
      return;
    }
    finally
    {
      Console.WriteLine("[Monitoring, Orders] Leaving OrderService.PlaceOrder");
    }
  }
  // This method only has [Log] from the fabric (default Category="Monitoring").
  public void GetOrderStatus(string orderId)
  {
    Console.WriteLine("[Monitoring] Entering OrderService.GetOrderStatus");
    try
    {
      Console.WriteLine("Business logic here.");
      return;
    }
    finally
    {
      Console.WriteLine("[Monitoring] Leaving OrderService.GetOrderStatus");
    }
  }
}