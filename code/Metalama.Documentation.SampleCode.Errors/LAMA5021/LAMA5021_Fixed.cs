// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;

namespace Doc.LAMA5021.Fixed;

[Immutable]
public class Point
{
    // Fixed: the property is get-only.
    public int X { get; }

    public int Y { get; }

    public Point( int x, int y )
    {
        this.X = x;
        this.Y = y;
    }
}
