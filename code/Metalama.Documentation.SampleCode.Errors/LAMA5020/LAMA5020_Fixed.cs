// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;

namespace Doc.LAMA5020.Fixed;

[Immutable]
public class Point
{
    // Fixed: the field is read-only.
    public readonly int X;

    public readonly int Y;

    public Point( int x, int y )
    {
        this.X = x;
        this.Y = y;
    }
}
