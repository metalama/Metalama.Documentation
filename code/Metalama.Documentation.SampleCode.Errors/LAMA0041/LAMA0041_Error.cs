// This is public domain Metalama sample code.

namespace Doc.LAMA0041.Error;

internal class Calculator
{
    // Error: the aspect throws an exception because Category isn't set.
    [Log]
    public int Add( int a, int b ) => a + b;
}
