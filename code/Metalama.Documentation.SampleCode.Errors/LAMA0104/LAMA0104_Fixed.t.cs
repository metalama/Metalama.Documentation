using System;
namespace Doc.LAMA0104.Fixed;
internal class Calculator
{
  [LogFirstParameter]
  public int Add(int a, int b)
  {
    Console.WriteLine("First parameter: a");
    return a + b;
  }
}