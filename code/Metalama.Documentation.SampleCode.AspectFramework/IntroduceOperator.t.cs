using System;
namespace Doc.IntroduceOperator;
[Addable]
internal partial class Vector2D
{
  public double X { get; }
  public double Y { get; }
  public Vector2D(double x, double y)
  {
    X = x;
    Y = y;
  }
  public override string ToString() => $"({X}, {Y})";
  public static Vector2D operator +(Vector2D left, Vector2D right)
  {
    return new Vector2D((double)left.X + (double)right.X, (double)left.Y + (double)right.Y);
  }
  public static Vector2D operator -(Vector2D value)
  {
    return new Vector2D(-(double)value.X, -(double)value.Y);
  }
}
internal class Program
{
  private static void Main()
  {
    var a = new Vector2D(1, 2);
    var b = new Vector2D(3, 4);
    Console.WriteLine(a + b);
    Console.WriteLine(-a);
  }
}