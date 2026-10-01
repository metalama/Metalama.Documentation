using System.Collections.Generic;
namespace Doc.CustomSyntaxSerializer;
[MemberCountAspect]
public class TargetClass
{
  public void Method1()
  {
  }
  public void Method1(int a)
  {
  }
  public void Method2()
  {
  }
  public Dictionary<string, MethodOverloadCount> GetMethodOverloadCount()
  {
    return new Dictionary<string, MethodOverloadCount>
    {
      {
        "Method1",
        new MethodOverloadCount("Method1", 2)
      },
      {
        "Method2",
        new MethodOverloadCount("Method2", 1)
      }
    };
  }
}