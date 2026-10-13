// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Patterns.Caching.Aspects.Configuration;
using System.IO;

namespace Doc.LAMA5111.Error;

public class StreamParameterClassifier : ICacheParameterClassifier
{
    // A stream can't be part of a cache key, so methods with a Stream parameter can't be cached.
    public CacheParameterClassification GetClassification( IParameter parameter )
        => parameter.Type.IsConvertibleTo( typeof(Stream) )
            ? CacheParameterClassification.Ineligible()
            : CacheParameterClassification.Default;
}
