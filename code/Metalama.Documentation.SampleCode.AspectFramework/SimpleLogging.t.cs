using System;
namespace Doc.SimpleLogging;
internal class Foo
{
  [SimpleLog]
  public void Method1()
  {
    Console.WriteLine("Entering Foo.Method1()");
    try
    {
      Console.WriteLine("Hello, world.");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving Foo.Method1()");
    }
  }
}