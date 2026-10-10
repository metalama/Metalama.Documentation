using System;
namespace Doc.LAMA0246.Fixed;
internal class Calculator
{
  [LogResult]
  public int Add(int a, int b)
  {
    void PrintResult(object? value)
    {
      Console.WriteLine($"Returning {value}.");
    }
    int result;
    result = a + b;
    PrintResult(result);
    return result;
  }
}