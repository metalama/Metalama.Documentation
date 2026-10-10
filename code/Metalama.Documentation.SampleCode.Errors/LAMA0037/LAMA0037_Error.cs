// This is public domain Metalama sample code.

namespace Doc.LAMA0037.Error;

internal class Calculator
{
    // Error: the [Log] aspect is not eligible on static methods.
    [Log]
    public static int Add( int a, int b ) => a + b;
}
