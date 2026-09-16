using System;
namespace Doc.Tags_Property;
[TagsAspect]
internal class Foo
{
  private int _a, _b;
  public int Sum => this._a + this._b;
  private void PrintInfo()
  {
    Console.WriteLine("This method has 2 fields and 1 properties.");
  }
}