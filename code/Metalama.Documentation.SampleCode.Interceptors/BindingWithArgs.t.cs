using System;
namespace Doc.BindingWithArgs;
[AddMetricContext]
public class OrderService
{
  public void PlaceOrder() => MetricsWithContext.Increment(name: "orders.placed", category: "Sales", weight: 2, valueType: typeof(decimal), unit: default);
}
public static class Metrics
{
  public static void Increment(string name) => Console.WriteLine($"{name} +1");
}
internal class Program
{
  private static void Main() => new OrderService().PlaceOrder();
}