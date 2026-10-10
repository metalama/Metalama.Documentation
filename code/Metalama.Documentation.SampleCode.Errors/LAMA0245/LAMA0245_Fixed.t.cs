using System;
namespace Doc.LAMA0245.Fixed;
[PrintDefaultValue]
internal partial class Counter
{
  public int Value { get; set; }
  public void PrintDefaultValue()
  {
    Console.WriteLine(default(int));
  }
}