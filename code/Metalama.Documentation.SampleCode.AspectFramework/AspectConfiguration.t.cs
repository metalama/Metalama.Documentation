using System.Diagnostics;
namespace Doc.AspectConfiguration
{
  // Some target code.
  public class SomeClass
  {
    [Log]
    public void SomeMethod()
    {
      Trace.TraceInformation("GeneralCategory: Executing SomeClass.SomeMethod().");
    }
  }
  namespace ChildNamespace
  {
    public class SomeOtherClass
    {
      [Log]
      public void SomeMethod()
      {
        Trace.TraceInformation("ChildCategory: Executing SomeOtherClass.SomeMethod().");
      }
    }
  }
}