// Warning LAMA5020 on `X`: `The 'Point.X' field must be read-only because of the [Immutable] aspect.`
using Metalama.Patterns.Immutability;
namespace Doc.LAMA5020.Error;
[Immutable]
public class Point
{
  // Warning: the field of an immutable type is not read-only.
  public int X;
  public readonly int Y;
  public Point(int x, int y)
  {
    this.X = x;
    this.Y = y;
  }
}