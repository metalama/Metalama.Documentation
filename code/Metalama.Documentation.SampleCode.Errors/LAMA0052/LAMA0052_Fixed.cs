// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(12)
#endif

// Fixed: this project is compiled with <LangVersion>12</LangVersion>, a version that Metalama supports.
namespace Doc.LAMA0052.Fixed
{
    public class Calculator
    {
        [Log]
        public int Add( int a, int b ) => a + b;
    }
}
