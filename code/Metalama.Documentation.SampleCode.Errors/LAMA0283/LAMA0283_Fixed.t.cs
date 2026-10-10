using System;
namespace Doc.LAMA0283.Fixed;
public class Calculator
{
  [Log("Math")]
  public int Add(int a, int b)
  {
    Console.WriteLine("[Math] Entering Calculator.Add(int, int).");
    return a + b;
  }
}