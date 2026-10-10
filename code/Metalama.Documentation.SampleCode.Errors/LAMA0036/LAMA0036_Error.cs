// This is public domain Metalama sample code.

namespace Doc.LAMA0036.Error;

internal class Calculator
{
    [TimedLog]
    public int Add( int a, int b ) => a + b;
}
