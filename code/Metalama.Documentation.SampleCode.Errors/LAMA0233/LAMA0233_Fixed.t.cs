using System;
namespace Doc.LAMA0233.Fixed;
internal class OrderService
{
  [Log]
  public void PlaceOrder(int orderId)
  {
    Console.WriteLine($"Entering OrderService.PlaceOrder(int) on {this}.");
  }
  [Log]
  public static void PurgeOrders()
  {
    Console.WriteLine($"Entering OrderService.PurgeOrders() on {("OrderService")}.");
  }
}