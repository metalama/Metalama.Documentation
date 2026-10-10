using System;
namespace Doc.LAMA0284.Fixed;
public class Calculator
{
  [LogReturnValue]
  public int Add(int a, int b)
  {
    var log = new Action<object?>(value => Console.WriteLine(value?.ToString()));
    int result;
    result = a + b;
    log(result);
    return result;
  }
}