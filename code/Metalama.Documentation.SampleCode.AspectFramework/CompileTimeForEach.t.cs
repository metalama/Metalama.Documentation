using System;
namespace Doc.CompileTimeForEach;
internal class Foo
{
  [CompileTimeForEach]
  private void Bar(int a, string b)
  {
    Console.WriteLine($"a = {a}");
    Console.WriteLine($"b = {b}");
    Console.WriteLine($"Hello, world! a={a}, b='{b}'.");
  }
}