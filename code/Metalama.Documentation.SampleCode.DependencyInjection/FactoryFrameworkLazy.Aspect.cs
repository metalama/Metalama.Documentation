// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.FactoryFrameworkLazy;

// A logging aspect that introduces a lazy logger dependency.
// The custom framework will store Func<IServiceFactory<ILogger>> and call Create() on first access.
public class LogLazyAttribute : OverrideMethodAspect
{
    [IntroduceDependency( IsLazy = true )]
    private readonly ILogger _logger;

    public override dynamic? OverrideMethod()
    {
        _logger.Log( $"Entering {meta.Target.Method}" );

        try
        {
            return meta.Proceed();
        }
        finally
        {
            _logger.Log( $"Leaving {meta.Target.Method}" );
        }
    }
}
