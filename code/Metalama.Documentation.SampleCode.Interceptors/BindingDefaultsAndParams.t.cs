using System;
namespace Doc.BindingDefaultsAndParams;
[RedirectLogging]
public class OrderProcessor
{
  public void Process(int orderId)
  {
    StructuredLog.Write(format: "Processing an order.", args: [], severity: Severity.Info);
    StructuredLog.Write(format: "Order {0} shipped to {1}.", severity: Severity.Warning, args: [orderId, "Paris"]);
  }
}
public enum Severity
{
  Info,
  Warning
}
public static class Log
{
  public static void Write(string format, Severity severity = Severity.Info, params object[] args) => Console.WriteLine($"{severity}: {string.Format(format, args)}");
}