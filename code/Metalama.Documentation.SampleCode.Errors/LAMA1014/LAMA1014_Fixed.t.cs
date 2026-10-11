using System;
namespace Doc.LAMA1014.Fixed;
internal static class Logger
{
  public static void Write(object message) => Console.WriteLine(message);
}
[InterceptLogging]
internal static class Checkout
{
  public static void Run() => Logger_Write_Interceptor(message: 42);
  private static void Logger_Write_Interceptor<TValue>(TValue message)
  {
    Console.WriteLine("Writing a message.");
    Logger.Write((object)message);
  }
}