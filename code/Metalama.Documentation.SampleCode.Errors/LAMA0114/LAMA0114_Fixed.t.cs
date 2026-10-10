using System;
namespace Doc.LAMA0114.Fixed;
internal class Calculator
{
  private int _total;
  [Trace]
  public int Add(int value)
  {
    Console.WriteLine($"Executing Add on {this}.");
    return this._total += value;
  }
  // Fixed: the template of [Trace] no longer uses meta.This on static methods.
  [Trace]
  public static int Multiply(int a, int b)
  {
    Console.WriteLine("Executing Multiply.");
    return a * b;
  }
}