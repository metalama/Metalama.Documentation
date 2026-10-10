// This is public domain Metalama sample code.

namespace Doc.LAMA0041.Fixed;

internal class Calculator
{
    // Fixed: Category is set, so the aspect doesn't throw.
    [Log( Category = "Math" )]
    public int Add( int a, int b ) => a + b;
}
