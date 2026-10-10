// This is public domain Metalama sample code.

namespace Doc.LAMA0263.Error;

internal class Calculator
{
    [LogFirstArgument]
    public int Square( int value ) => value * value;
}
