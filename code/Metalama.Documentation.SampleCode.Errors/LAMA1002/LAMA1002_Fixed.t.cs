using System;
namespace Doc.LAMA1002.Fixed;
public static class LegacyLogger
{
  public static void Write(string message) => Console.WriteLine(message);
}
public static class Logger
{
  public static void Write(string message) => Console.WriteLine($"[{DateTime.Now:T}] {message}");
}
[FlushLog]
internal partial class OrderService
{
  public void Place() => Logger.Write("Order placed.");
  public void Flush()
  {
    LegacyLogger.Write("Flushed.");
  }
}