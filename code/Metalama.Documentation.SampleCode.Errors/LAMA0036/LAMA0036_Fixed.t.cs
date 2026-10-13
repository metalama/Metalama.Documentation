using System;
namespace Doc.LAMA0036.Fixed;
internal class Calculator
{
  [TimedLog]
  public int Add(int a, int b)
  {
    Console.WriteLine($"{(DateTime.Now):T} Executing Calculator.Add(int, int).");
    return a + b;
  }
}