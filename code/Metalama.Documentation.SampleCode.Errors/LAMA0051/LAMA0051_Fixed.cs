// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(13)
#endif

// Fixed: this project is compiled with <LangVersion>13</LangVersion>, a version that Metalama supports.
namespace Doc.LAMA0051.Fixed
{
    public class Calculator
    {
        [Log]
        public int Add( int a, int b ) => a + b;
    }
}
