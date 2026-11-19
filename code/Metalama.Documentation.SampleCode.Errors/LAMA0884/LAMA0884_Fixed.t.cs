using System;
namespace Doc.LAMA0884.Fixed;
internal class OrderService
{
  [Log("Orders", "Billing")]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("[Orders, Billing] Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
}