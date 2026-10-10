using System;
namespace Doc.LAMA0253.Fixed;
internal class OrderService
{
  [Audit]
  public void PlaceOrder(string productId, int quantity)
  {
    AuditLog.Write("PlaceOrder");
    Console.WriteLine($"Ordering {quantity} x {productId}.");
  }
}
public static class AuditLog
{
  public static void Write(object operation) => Console.WriteLine($"Audit: {operation}");
}