// This is public domain Metalama sample code.

namespace Doc.LAMA0284.Error;

public class Calculator
{
    [LogReturnValue]
    public int Add( int a, int b ) => a + b;
}
