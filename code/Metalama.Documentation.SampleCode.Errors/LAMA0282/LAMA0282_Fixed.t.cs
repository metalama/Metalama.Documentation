using System;
namespace Doc.LAMA0282.Fixed;
public class Calculator
{
  // The [Log] aspect now uses only C# 10 features.
  [Log]
  public int Add(int a, int b)
  {
    // Logged by the "Log" aspect.
    Console.WriteLine("Entering Calculator.Add(int, int).");
    return a + b;
  }
}