// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(8.0)
#endif

// This project is compiled with <LangVersion>8.0</LangVersion>, which Metalama does not support.
namespace Doc.LAMA0052.Error
{
    public class Calculator
    {
        [Log]
        public int Add( int a, int b ) => a + b;
    }
}
