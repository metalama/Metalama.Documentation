using System;
using System.Collections.Generic;
namespace Doc.LAMA0227.Fixed;
internal class Calculator
{
  [LogArguments]
  public int Add(int a, int b)
  {
    var values = new List<object?>(new object[] { a, b });
    Console.WriteLine($"Add({string.Join(", ", values)})");
    return a + b;
  }
}