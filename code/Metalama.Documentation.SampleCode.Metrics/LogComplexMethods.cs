// This is public domain Metalama sample code.

namespace Doc.LogComplexMethods;

internal class Calculator
{
    // Simple method: ~15 syntax nodes - will NOT be logged.
    public int Add( int a, int b )
    {
        return a + b;
    }

    // Complex method: ~65 syntax nodes - will be logged.
    public int Fibonacci( int n )
    {
        if ( n <= 0 )
        {
            return 0;
        }

        if ( n == 1 )
        {
            return 1;
        }

        var prev = 0;
        var current = 1;

        for ( var i = 2; i <= n; i++ )
        {
            var next = prev + current;
            prev = current;
            current = next;
        }

        return current;
    }
}
