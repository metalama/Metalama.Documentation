// Warning MY001 on `new`: `'Doc.ForTestOnly' can only be invoked from a namespace that ends with Tests.`
using System;
namespace Doc.ForTestOnly
{
  public class MyService
  {
    // Normal constructor.
    public MyService() : this(DateTime.Now)
    {
    }
    [ForTestOnly]
    internal MyService(DateTime dateTime)
    {
    }
  }
  internal class NormalClass
  {
    // Usage NOT allowed here because we are not in a Tests namespace.
    private MyService _service = new(DateTime.Now.AddDays(1));
  }
  namespace Tests
  {
    internal class TestClass
    {
      // Usage allowed here because we are in a Tests namespace.
      private MyService _service = new(DateTime.Now.AddDays(2));
    }
  }
}