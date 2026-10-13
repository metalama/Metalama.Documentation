using System;
namespace Doc.LAMA0287.Fixed;
internal class Calculator
{
  private int _total;
  [Trace]
  public int Add(int value)
  {
    Console.WriteLine($"Executing Add on {this}.");
    return this._total += value;
  }
  // Fixed: the template of [Trace] no longer uses meta.Receiver on methods without a receiver.
  [Trace]
  public static int Multiply(int a, int b)
  {
    Console.WriteLine("Executing Multiply.");
    return a * b;
  }
}