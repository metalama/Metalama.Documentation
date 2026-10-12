// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;

namespace Doc.LAMA5021.Error;

[Immutable]
public class Point
{
    // Warning: the automatic property of an immutable type has a setter.
    public int X { get; set; }

    public int Y { get; }

    public Point( int x, int y )
    {
        this.X = x;
        this.Y = y;
    }
}
