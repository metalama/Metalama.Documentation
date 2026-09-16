using System;
namespace Doc.Testing;
internal class SimpleLogTests
{
  [SimpleLog]
  private void MyMethod()
  {
    Console.WriteLine("Entering SimpleLogTests.MyMethod()");
    try
    {
      Console.WriteLine("Hello, world");
      return;
    }
    finally
    {
      Console.WriteLine("Leaving SimpleLogTests.MyMethod()");
    }
  }
}