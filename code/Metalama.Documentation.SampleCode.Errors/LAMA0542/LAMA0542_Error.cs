// This is public domain Metalama sample code.

#if TEST_OPTIONS
// @LanguageVersion(preview)
#endif

namespace Doc.LAMA0542.Error;

// Error: the aspect introduces an indexer with an init accessor into the extension block.
[ItemIndexer]
internal static class IntExtensions
{
    extension( int number )
    {
    }
}
