// Warning LAMA0661 on `id`: `The lambda parameter 'id' is renamed to 'id_1' in the compiled code, because it hides the parameter 'id' of 'OrderProcessor.Process(int, int[])', and this parameter is passed by the call 'LegacyLogger.Write( $"Item {id}." )' redirected to 'Logger.Write(string, int)' by aspect [UseModernLoggerAttribute] applied to 'OrderProcessor'. The compiled code no longer matches the source code, so the debugger shows the new name. Rename the lambda parameter in the source code to remove this warning.`
using System;
namespace Doc.LAMA0661.Error;
public static class LegacyLogger
{
  public static void Write(string message) => Console.WriteLine(message);
}
public static class Logger
{
  public static void Write(string message, int requestId) => Console.WriteLine($"{message} (request {requestId})");
}
[UseModernLogger]
public class OrderProcessor
{
  public void Process(int id, int[] items)
  {
    Logger.Write(message: "Processing started.", requestId: id);
    // The lambda parameter hides the parameter of the method.
    Array.ForEach(items, id_1 => Logger.Write(message: $"Item {id_1}.", requestId: id));
  }
}