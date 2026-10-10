// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0202.Error;

public class MultiplicationTableAttribute : TypeAspect
{
    // Introduces a method that returns a lookup table computed at compile time.
    [Introduce]
    public object GetMultiplicationTable()
    {
        object table = TableBuilder.Build( 3 );

        // Error: a multidimensional array can't be converted to a run-time value.
        return meta.RunTime( table );
    }
}

[CompileTime]
public static class TableBuilder
{
    // Builds a two-dimensional multiplication table.
    public static int[,] Build( int size )
    {
        var table = new int[size, size];

        for ( var i = 0; i < size; i++ )
        {
            for ( var j = 0; j < size; j++ )
            {
                table[i, j] = (i + 1) * (j + 1);
            }
        }

        return table;
    }
}
