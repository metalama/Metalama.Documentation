using System;
namespace Doc.LogParameters;
internal class Foo
{
  [Log]
  private void VoidMethod(int a, out int b)
  {
    Console.WriteLine($"Foo.VoidMethod(a = {{{a}}}, b = <out> ) started.");
    try
    {
      b = a;
      object result = null;
      Console.WriteLine($"Foo.VoidMethod(a = {{{a}}}, b = <out> ) succeeded.");
      return;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Foo.VoidMethod(a = {{{a}}}, b = <out> ) failed: {e.Message}");
      throw;
    }
  }
  [Log]
  private int IntMethod(int a)
  {
    Console.WriteLine($"Foo.IntMethod(a = {{{a}}}) started.");
    try
    {
      int result;
      result = a;
      Console.WriteLine($"Foo.IntMethod(a = {{{a}}}) returned {result}.");
      return result;
    }
    catch (Exception e)
    {
      Console.WriteLine($"Foo.IntMethod(a = {{{a}}}) failed: {e.Message}");
      throw;
    }
  }
}