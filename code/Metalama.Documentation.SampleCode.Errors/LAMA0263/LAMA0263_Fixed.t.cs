using System;
namespace Doc.LAMA0263.Fixed;
internal class Calculator
{
  [LogFirstArgument]
  public int Square(int value)
  {
    Func<object?> getArgument = () => (object? )value;
    Console.WriteLine($"First argument: {getArgument()}");
    return value * value;
  }
}