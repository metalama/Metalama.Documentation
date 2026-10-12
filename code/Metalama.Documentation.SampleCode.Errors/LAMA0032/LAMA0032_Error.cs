// This is public domain Metalama sample code.

namespace Doc.LAMA0032.Error;

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => a + b;

    [Log( Category = "Math" )]
    public int Multiply( int a, int b ) => a * b;
}
