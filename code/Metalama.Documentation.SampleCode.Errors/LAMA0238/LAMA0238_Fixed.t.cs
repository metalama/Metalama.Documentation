using System;
namespace Doc.LAMA0238.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("[General] Entering Calculator.Add(int, int).");
    return a + b;
  }
}