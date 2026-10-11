// This is public domain Metalama sample code.

namespace Doc.LAMA0037.Fixed;

internal class Calculator
{
    // Fixed: the method is no longer static, so the aspect is eligible.
    [Log]
    public int Add( int a, int b ) => a + b;
}
