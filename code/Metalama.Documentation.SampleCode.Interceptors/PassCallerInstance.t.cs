using System;
namespace Doc.PassCallerInstance;
public class OrderService
{
  public void PlaceOrder(string product) => TelemetryWithSender.Track(eventName: $"Order placed: {product}", sender: this);
  public static void Reset() => TelemetryWithSender.Track(eventName: "Orders reset", sender: default);
}
public static class Telemetry
{
  public static void Track(string eventName) => Console.WriteLine(eventName);
}