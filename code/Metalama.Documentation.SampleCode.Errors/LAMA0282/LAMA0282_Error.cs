// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(10)
#endif

namespace Doc.LAMA0282.Error;

public class Calculator
{
    // The [Log] aspect uses C# 11 features.
    [Log]
    public int Add( int a, int b ) => a + b;
}
