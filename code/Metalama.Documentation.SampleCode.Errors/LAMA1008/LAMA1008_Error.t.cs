// Warning LAMA1008 on `Fabric`: `The interceptor method 'Interceptors.Print(string)' registered by the fabric Doc.LAMA1008.Error.Fabric intercepts methods named 'Prnt' declared by 'Printer', but no such method exists.`
using System;
namespace Doc.LAMA1008.Error;
internal static class Printer
{
  public static void Print(string value) => Console.WriteLine(value);
}
internal static class Program
{
  public static void Run() => Printer.Print("Hello");
}