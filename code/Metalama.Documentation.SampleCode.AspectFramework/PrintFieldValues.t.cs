using System;
namespace Doc.PrintFieldValues;
internal class Foo
{
  private readonly int _a;
  public string? B { get; set; }
  private static readonly int _c;
  [PrintFieldValues]
  public void Bar()
  {
    var value = _a;
    Console.WriteLine($"_a={value}");
    var value_1 = _c;
    Console.WriteLine($"_c={value_1}");
    var value_2 = B;
    Console.WriteLine($"B={value_2}");
  }
}