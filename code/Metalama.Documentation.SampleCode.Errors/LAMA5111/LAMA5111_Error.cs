// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.IO;

namespace Doc.LAMA5111.Error;

public class DocumentService
{
    // Error: the classifier marks the Stream parameter as ineligible.
    [Cache]
    public int CountLines( Stream stream )
    {
        using var reader = new StreamReader( stream );

        return reader.ReadToEnd().Split( '\n' ).Length;
    }
}
