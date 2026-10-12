using System;
namespace Doc.LAMA0022.Fixed;
[LogAllMethods]
internal class Calculator
{
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
  public int Subtract(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Subtract(int, int).");
    return a - b;
  }
}