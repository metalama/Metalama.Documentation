using System;
namespace Doc.ThrowOnCall
{
  public class Foo
  {
    [ThrowOnCall]
    private static void OldImplementation()
    {
      throw new Exception("Method Foo.OldImplementation() is obsolete.");
    }
    public static void Main()
    {
      OldImplementation();
    }
  }
}