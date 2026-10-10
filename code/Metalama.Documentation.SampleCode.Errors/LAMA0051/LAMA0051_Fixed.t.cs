using System;
namespace Doc.LAMA0051.Fixed
{
  public class Calculator
  {
    [Log]
    public int Add(int a, int b)
    {
      Console.WriteLine("Executing Calculator.Add(int, int).");
      return a + b;
    }
  }
}