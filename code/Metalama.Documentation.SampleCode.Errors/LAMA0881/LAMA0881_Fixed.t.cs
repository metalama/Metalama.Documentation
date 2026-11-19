using System;
namespace Doc.LAMA0881.Fixed;
internal class OrderService
{
  [Log(Category = "Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}