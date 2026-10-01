namespace Doc.ToString;
[ToString]
internal class Foo
{
  private int _x;
  public string? Y { get; set; }
  public override string ToString()
  {
    return $"{{ Foo _x={_x}, Y={Y} }}";
  }
}