using System.Diagnostics;
namespace Doc.AspectConfiguration_Provider;
[LogConfiguration(Category = "SomeClass")]
public class SomeClass
{
  [Log]
  public void SomeMethod()
  {
    Trace.WriteLine("SomeClass: Executing SomeClass.SomeMethod().");
  }
}
[LogConfiguration(Category = "SomeClass")]
public class ChildNamespace
{
  public class SomeOtherClass
  {
    [Log(Level = TraceLevel.Warning)]
    public void SomeMethod()
    {
      Trace.TraceWarning("SomeClass: Executing ChildNamespace.SomeOtherClass.SomeMethod().");
    }
  }
}