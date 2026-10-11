using System;
namespace Doc.LAMA0071.Fixed;
internal class OrderService
{
  [Audit]
  public void PlaceOrder(int productId)
  {
    WriteAudit<OrderService>("PlaceOrder");
  }
  public void WriteAudit<TCategory>(string operation)
  {
    Console.WriteLine($"[{typeof(TCategory).Name}] {operation}");
  }
}