// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(preview)
#endif

// This project is compiled with <LangVersion>preview</LangVersion>, which Metalama does not support.
namespace Doc.LAMA0051.Error
{
    public class Calculator
    {
        [Log]
        public int Add( int a, int b ) => a + b;
    }
}
