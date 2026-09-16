using System;
namespace Doc.GettingStarted
{
  internal class Foo
  {
    [Log]
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
}