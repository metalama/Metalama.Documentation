using System;
namespace Doc.LAMA0041.Fixed;
internal class Calculator
{
  // Fixed: Category is set, so the aspect doesn't throw.
  [Log(Category = "Math")]
  public int Add(int a, int b)
  {
    Console.WriteLine("Math: executing Calculator.Add(int, int).");
    return a + b;
  }
}