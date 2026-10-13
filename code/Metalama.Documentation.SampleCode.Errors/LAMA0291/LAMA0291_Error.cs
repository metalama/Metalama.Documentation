// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @RoslynIsCompileTimeOnly
#endif

namespace Doc.LAMA0291.Error;

internal class Calculator
{
    [Log]
    public int Add( int a, int b ) => a + b;
}
