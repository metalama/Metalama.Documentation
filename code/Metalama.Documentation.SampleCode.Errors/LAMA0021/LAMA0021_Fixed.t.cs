using System;
using System.Diagnostics;
using Doc.LAMA0021.Fixed;
using Metalama.Framework.Aspects;
namespace Doc.LAMA0021.Fixed;
internal class Calculator
{
  [Log]
  [Measure]
  public int Add(int a, int b)
  {
    Console.WriteLine("Executing Calculator.Add(int, int).");
    var stopwatch = Stopwatch.StartNew();
    try
    {
      return a + b;
    }
    finally
    {
      Console.WriteLine($"Calculator.Add(int, int) took {stopwatch.ElapsedMilliseconds} ms.");
    }
  }
}