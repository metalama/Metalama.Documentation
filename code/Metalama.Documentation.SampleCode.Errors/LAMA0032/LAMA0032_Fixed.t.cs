using System;
namespace Doc.LAMA0032.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
  [Log(Category = "Math")]
  public int Multiply(int a, int b)
  {
    Console.WriteLine("Math: executing Calculator.Multiply(int, int).");
    return a * b;
  }
}