using System;
namespace Doc.LAMA0276.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    Console.WriteLine("Add returns Int32.");
    return a + b;
  }
}