// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0202.Fixed;

public class MultiplicationTableAttribute : TypeAspect
{
    // Introduces a method that returns a lookup table computed at compile time.
    [Introduce]
    public object GetMultiplicationTable()
    {
        object table = TableBuilder.Build( 3 );

        // Fixed: a jagged array can be converted to a run-time value.
        return meta.RunTime( table );
    }
}

[CompileTime]
public static class TableBuilder
{
    // Fixed: builds a jagged array instead of a two-dimensional array.
    public static int[][] Build( int size )
    {
        var table = new int[size][];

        for ( var i = 0; i < size; i++ )
        {
            table[i] = new int[size];

            for ( var j = 0; j < size; j++ )
            {
                table[i][j] = (i + 1) * (j + 1);
            }
        }

        return table;
    }
}
