using System;
namespace Doc.LAMA0055.Fixed;
// Fixed: the [Log] aspect is applied to the type, and it adds itself to the methods.
[Log]
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