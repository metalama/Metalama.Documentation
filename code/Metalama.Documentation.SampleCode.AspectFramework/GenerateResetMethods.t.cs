namespace Doc.GenerateResetMethods;
[GenerateResetMethods]
public class Foo
{
  private int _x;
  public string Y { get; set; }
  public string Z => this.Y;
  public void ResetX()
  {
    _x = default(int);
  }
  public void ResetY()
  {
    Y = default(string);
  }
}