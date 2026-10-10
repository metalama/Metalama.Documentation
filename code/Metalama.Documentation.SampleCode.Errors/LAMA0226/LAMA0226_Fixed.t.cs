using System;
using System.Collections.Generic;
namespace Doc.LAMA0226.Fixed;
public class AuditEntry
{
  public AuditEntry(object? value)
  {
    this.Value = value;
  }
  public object? Value { get; }
}
public static class AuditLog
{
  public static void Write(string methodName, Dictionary<string, AuditEntry> entries) => Console.WriteLine($"{methodName} called with {entries.Count} argument(s).");
}
internal class OrderService
{
  [Audit]
  public void PlaceOrder(string product, int quantity)
  {
    var entries = new Dictionary<string, AuditEntry>();
    entries.Add("product", new AuditEntry(product));
    entries.Add("quantity", new AuditEntry(quantity));
    AuditLog.Write("PlaceOrder", entries);
  }
}