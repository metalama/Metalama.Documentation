using System;
namespace Doc.CallAfter;
public class TestClass
{
  private int _state;
  [CallAfter(nameof(CheckState))]
  public void Open()
  {
    this._state++;
    object result = null;
    CheckState();
  }
  [CallAfter(nameof(CheckState))]
  public void Close()
  {
    this._state--;
    object result = null;
    CheckState();
  }
  private void CheckState()
  {
    if (this._state < 0)
    {
      throw new InvalidOperationException();
    }
  }
}