using System;
namespace Doc.LAMA0285.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Entering Add.");
    return a + b;
  }
}