using System;
namespace Doc.LAMA1000.Fixed;
public static class LegacyLogger
{
  public static void Write(string message) => Console.WriteLine(message);
}
public static class Logger
{
  public static void Write(string message) => Console.WriteLine($"[{DateTime.Now:T}] {message}");
}
internal class Checkout
{
  public void Complete() => LegacyLogger.Write("Checkout completed.");
}
[RedirectLogging]
internal class OrderService
{
  public void Place() => Logger.Write("Order placed.");
}