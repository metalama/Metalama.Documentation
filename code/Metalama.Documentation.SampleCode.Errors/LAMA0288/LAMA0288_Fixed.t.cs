namespace Doc.LAMA0288.Fixed;
[ToString]
internal partial class Point
{
  public int X { get; set; }
  public int Y { get; set; }
  public override string ToString()
  {
    return "Point: X, Y";
  }
}