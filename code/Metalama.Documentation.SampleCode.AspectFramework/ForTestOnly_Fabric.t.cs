// Warning MY001 on `MyService`: `'Doc.ForTestOnly_Fabric.ValidatedNamespace' can only be invoked from a namespace that ends with '.Tests'.`
// Warning MY001 on `new`: `'Doc.ForTestOnly_Fabric.ValidatedNamespace' can only be invoked from a namespace that ends with '.Tests'.`
using Doc.ForTestOnly_Fabric.ValidatedNamespace;
namespace Doc.ForTestOnly_Fabric
{
  namespace ValidatedNamespace
  {
    public class MyService
    {
    }
  }
  internal class NormalClass
  {
    // Usage NOT allowed here.
    private MyService _service = new();
  }
  namespace Tests
  {
    internal class TestClass
    {
      // Usage allowed here because we are in the Tests namespace.
      private MyService _service = new();
    }
  }
}