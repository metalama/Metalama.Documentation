using System.Collections.Generic;
namespace Doc.LAMA0200.Fixed;
[MethodOverloadCount]
public partial class OrderService
{
  public void Submit()
  {
  }
  public void Submit(int priority)
  {
  }
  public void Cancel()
  {
  }
  public Dictionary<string, MethodOverloadCount> GetMethodOverloadCount()
  {
    return new Dictionary<string, MethodOverloadCount>
    {
      {
        "Submit",
        new MethodOverloadCount("Submit", 2)
      },
      {
        "Cancel",
        new MethodOverloadCount("Cancel", 1)
      }
    };
  }
}