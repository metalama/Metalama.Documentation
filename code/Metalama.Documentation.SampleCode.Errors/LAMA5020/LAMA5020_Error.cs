// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;

namespace Doc.LAMA5020.Error;

[Immutable]
public class Point
{
    // Warning: the field of an immutable type is not read-only.
    public int X;

    public readonly int Y;

    public Point( int x, int y )
    {
        this.X = x;
        this.Y = y;
    }
}
