// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(10)
#endif

namespace Doc.LAMA0282.Fixed;

public class Calculator
{
    // The [Log] aspect now uses only C# 10 features.
    [Log]
    public int Add( int a, int b ) => a + b;
}
