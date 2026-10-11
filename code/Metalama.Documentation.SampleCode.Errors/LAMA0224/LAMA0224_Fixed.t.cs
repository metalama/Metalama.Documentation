using System;
namespace Doc.LAMA0224.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    try
    {
      int result;
      result = a + b;
      Console.WriteLine($"Add returned {result}.");
      return result;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Add failed: {e.Message}");
      throw;
    }
  }
}