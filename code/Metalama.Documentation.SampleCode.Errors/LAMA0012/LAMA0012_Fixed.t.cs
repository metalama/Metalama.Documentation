using System;
namespace Doc.LAMA0012.Fixed;
internal class OrderService
{
  [Audit]
  public void PlaceOrder(string product, int quantity)
  {
    WriteAudit("OrderService", "PlaceOrder");
    Console.WriteLine($"Placing an order for {quantity} {product}.");
  }
  private void WriteAudit(string typeName, string methodName)
  {
    Console.WriteLine($"Audit: {typeName}.{methodName}");
  }
}