// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Patterns.Caching.Aspects.Configuration;
using Microsoft.Extensions.Logging;

namespace Doc.ParameterFilter;

public class LoggerParameterClassifier : ICacheParameterClassifier
{
    public CacheParameterClassification GetClassification( IParameter parameter )
        => parameter.Type.IsConvertibleTo( typeof(ILogger) )
            ? CacheParameterClassification.ExcludeFromCacheKey
            : CacheParameterClassification.Default;
}