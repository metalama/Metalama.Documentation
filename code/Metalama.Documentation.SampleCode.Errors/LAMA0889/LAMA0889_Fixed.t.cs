using System;
namespace Doc.LAMA0889.Fixed;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}