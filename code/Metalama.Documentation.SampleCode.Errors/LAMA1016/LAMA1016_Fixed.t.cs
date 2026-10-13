using System;
namespace Doc.LAMA1016.Fixed;
public static class Printer
{
  public static void Print(string value) => Console.WriteLine(value);
}
[InterceptPrint]
public static class Report
{
  public static void Run() => Printer_Print_Interceptor(value: "Hello", line: 42);
  private static void Printer_Print_Interceptor(string value, int line)
  {
    Console.Write($"[line {line}] ");
    Printer.Print(value);
  }
}