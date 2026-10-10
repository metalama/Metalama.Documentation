// This is public domain Metalama sample code.

using Metalama.Patterns.Caching.Aspects;
using System.IO;

namespace Doc.LAMA5111.Fixed;

public class DocumentService
{
    // Fixed: the method receives the file path, which can be part of the cache key.
    [Cache]
    public int CountLines( string path )
    {
        return File.ReadAllLines( path ).Length;
    }
}
