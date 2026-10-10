// Warning LAMA5021 on `X`: `The 'Point.X' property must not have a setter because of the [Immutable] aspect.`
using Metalama.Patterns.Immutability;
namespace Doc.LAMA5021.Error;
[Immutable]
public class Point
{
  // Warning: the automatic property of an immutable type has a setter.
  public int X { get; set; }
  public int Y { get; }
  public Point(int x, int y)
  {
    this.X = x;
    this.Y = y;
  }
}