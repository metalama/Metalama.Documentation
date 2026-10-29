// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The dynamic field also violates the immutability contract, which is not the topic of this sample.
// @IgnoredDiagnostic(LAMA0882)
#endif

namespace Doc.LAMA0238.Error;

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => a + b;
}
