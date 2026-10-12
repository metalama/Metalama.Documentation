using System;
namespace Doc.LAMA1004.Fixed;
internal static class Printer
{
  public static void Print(string value) => Console.WriteLine(value);
}
[RedirectPrints]
internal static class Program
{
  public static void Run() => Interceptors.Print("Hello");
}