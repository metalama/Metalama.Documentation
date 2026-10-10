// This is public domain Metalama sample code.

namespace Doc.LAMA0055.Error;

internal class Calculator
{
    // The [Log] aspect tries to add itself to the Calculator type.
    [Log]
    public int Add( int a, int b ) => a + b;

    public int Subtract( int a, int b ) => a - b;
}
