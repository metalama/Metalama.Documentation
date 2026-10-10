using System;
namespace Doc.LAMA0013.Fixed;
internal class OrderService
{
  [Audit]
  public void PlaceOrder(string product, int quantity)
  {
    WriteAudit("PlaceOrder");
    Console.WriteLine($"Placing an order for {quantity} {product}.");
  }
  private void WriteAudit(string methodName, params object? [] details)
  {
    Console.WriteLine($"Audit: {methodName} ({details.Length} details)");
  }
}