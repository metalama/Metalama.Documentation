using System;
namespace Doc.Decoupled;
public class C
{
  public void UnmarkedMethod()
  {
  }
  [Log(Category = "Foo")]
  public void MarkedMethod()
  {
    Console.WriteLine("[Foo] Executing C.MarkedMethod()");
  }
  private string _markedProperty = default !;
  [Log(Category = "Bar")]
  public string MarkedProperty
  {
    get
    {
      return _markedProperty;
    }
    set
    {
      Console.WriteLine("[Bar] Executing C.MarkedProperty.set");
      _markedProperty = value;
    }
  }
}