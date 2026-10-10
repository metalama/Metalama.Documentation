using System;
namespace Doc.LAMA0037.Fixed;
internal class Calculator
{
  // Fixed: the method is no longer static, so the aspect is eligible.
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
}