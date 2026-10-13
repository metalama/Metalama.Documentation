using System;
namespace Doc.LAMA0265.Fixed;
[AddPrintZero]
internal partial class Calculator
{
  public int Add(int a, int b) => a + b;
  public void PrintZero()
  {
    Console.WriteLine(NumberHelper.Zero<int>());
  }
}