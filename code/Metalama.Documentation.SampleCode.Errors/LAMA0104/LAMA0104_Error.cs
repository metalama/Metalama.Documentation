// This is public domain Metalama sample code.

namespace Doc.LAMA0104.Error;

internal class Calculator
{
    [LogFirstParameter]
    public int Add( int a, int b ) => a + b;
}
