using System;
namespace Doc.LAMA0232.Fixed;
public class Calculator
{
  // The [Log] aspect now uses only C# 10 features.
  [Log]
  public int Add(int a, int b)
  {
    var message = "Logged by the \"Log\" aspect.";
    Console.WriteLine(message);
    return a + b;
  }
}