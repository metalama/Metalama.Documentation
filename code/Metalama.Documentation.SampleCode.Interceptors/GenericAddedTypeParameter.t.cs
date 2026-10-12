using System;
namespace Doc.GenericAddedTypeParameter;
[TraceLogging]
public class OrderService
{
  public void Place(int quantity, string product)
  {
    MetalamaInterceptors.Log_Write_Interceptor(value: quantity);
    MetalamaInterceptors.Log_Write_Interceptor(value: product);
  }
}
public static class Log
{
  // The parameter is an object, so a value type is boxed at the call site.
  public static void Write(object value) => Console.WriteLine(value);
}
internal class Program
{
  private static void Main() => new OrderService().Place(3, "Book");
}