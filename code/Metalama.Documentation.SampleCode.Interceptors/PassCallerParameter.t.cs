using System;
using System.Linq;
namespace Doc.PassCallerParameter;
[AddRequestId]
public class OrderController
{
  // The calling method has a requestId parameter, which is passed to the interceptor.
  public void Get(string requestId, int orderId) => AuditLogWithRequest.Write(message: $"Reading order {orderId}.", requestId: requestId);
  // The call is in a lambda, which can also read requestId.
  public void GetMany(string requestId, int[] orderIds) => orderIds.ToList().ForEach(id => AuditLogWithRequest.Write(message: $"Reading order {id}.", requestId: requestId));
  // The calling method has no requestId parameter, so the call is not intercepted.
  public void Ping() => AuditLog.Write("Ping.");
}
public static class AuditLog
{
  public static void Write(string message) => Console.WriteLine(message);
}
internal class Program
{
  private static void Main()
  {
    var controller = new OrderController();
    controller.Get("r1", 42);
    controller.GetMany("r2", [1, 2]);
    controller.Ping();
  }
}