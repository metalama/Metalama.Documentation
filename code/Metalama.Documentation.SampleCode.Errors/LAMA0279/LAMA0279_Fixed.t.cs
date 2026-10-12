using System;
namespace Doc.LAMA0279.Fixed;
internal class Calculator
{
  [ConsoleLog]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
}