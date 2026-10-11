using System;
namespace Doc.LAMA1024.Fixed;
public static class Printer
{
  public static void Print(string value) => Console.WriteLine(value);
}
public static class Helpers
{
  internal static void Printer_Print_Interceptor(string value)
  {
    Console.Write("> ");
    Printer.Print(value);
  }
}
[InterceptPrint]
public static class Report
{
  public static void Run() => Helpers.Printer_Print_Interceptor(value: "Hello");
}