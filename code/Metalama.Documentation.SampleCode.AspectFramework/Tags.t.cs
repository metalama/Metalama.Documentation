using System;
namespace Doc.Tags;
internal class Foo
{
  [TagsAspect]
  private void Bar(int a, int b)
  {
    Console.WriteLine("This method has 2 parameters.");
    Console.WriteLine($"Method({a}, {b})");
  }
}