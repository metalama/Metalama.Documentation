using System;
namespace Doc.LAMA0273.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Entering Calculator.Add(int, int).");
    return a + b;
  }
}