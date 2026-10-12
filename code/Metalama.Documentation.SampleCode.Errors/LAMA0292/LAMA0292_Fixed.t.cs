using System;
namespace Doc.LAMA0292.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Entering Calculator::Add.");
    return a + b;
  }
}