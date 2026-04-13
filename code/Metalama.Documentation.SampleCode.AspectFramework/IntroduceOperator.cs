// This is public domain Metalama sample code.

using System;

namespace Doc.IntroduceOperator;

[Addable]
internal partial class Vector2D
{
    public double X { get; }
    public double Y { get; }

    public Vector2D( double x, double y )
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

internal class Program
{
    private static void Main()
    {
        var a = new Vector2D( 1, 2 );
        var b = new Vector2D( 3, 4 );

#if METALAMA
        Console.WriteLine( a + b );
        Console.WriteLine( -a );
#endif
    }
}
