using System;
namespace Doc.LAMA0269.Fixed;
internal class Calculator
{
  [Log]
  public int Add(int a, int b)
  {
    var entry = new
    {
      Name = "Calculator"
    };
    Console.WriteLine(entry);
    return a + b;
  }
}