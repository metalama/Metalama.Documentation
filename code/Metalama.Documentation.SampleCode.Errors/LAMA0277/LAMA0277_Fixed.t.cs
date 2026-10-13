using System;
namespace Doc.LAMA0277.Fixed;
[TypeLogger]
internal partial class Calculator
{
  public int Add(int a, int b) => a + b;
  public void LogType<T>()
  {
    Console.WriteLine($"Type: {typeof(T).Name}");
  }
}