using System;
namespace Doc.LAMA0887.Fixed;
internal class OrderService
{
  [Log]
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering PlaceOrder.");
  // Places the order.
  }
}