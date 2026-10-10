using System;
namespace Doc.LAMA0039.Fixed;
internal class Calculator
{
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    return a + b;
  }
  // The fabric skips this static method.
  public static int Multiply(int a, int b) => a * b;
}