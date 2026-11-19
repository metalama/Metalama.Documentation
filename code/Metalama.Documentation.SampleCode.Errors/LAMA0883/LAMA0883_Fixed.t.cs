using System;
namespace Doc.LAMA0883.Fixed;
internal class OrderService
{
  [Log("Orders")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}