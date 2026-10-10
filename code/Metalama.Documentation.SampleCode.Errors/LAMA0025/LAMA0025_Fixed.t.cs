using System;
namespace Doc.LAMA0025.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
}