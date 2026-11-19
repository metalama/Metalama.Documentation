using System;
namespace Doc.LAMA0890.Fixed;
[Log]
internal class OrderService : IDisposable
{
  public void PlaceOrder(string product, int quantity)
  {
    Console.WriteLine("Entering OrderService.PlaceOrder(string, int).");
  // Places the order.
  }
  public void Dispose()
  {
  }
}