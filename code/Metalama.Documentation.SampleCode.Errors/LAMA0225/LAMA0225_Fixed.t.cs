using System;
namespace Doc.LAMA0225.Fixed;
internal class Calculator
{
  [ReturnDefaultOnException]
  public int Divide(int a, int b)
  {
    var result = default(int);
    try
    {
      result = a / b;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Divide failed: {e.Message}");
    }
    return result;
  }
}