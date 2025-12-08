// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.FactoryFramework;

// A logging aspect that introduces a logger dependency.
// The custom framework will pull IServiceFactory<ILogger> and call Create().
public class LogAttribute : OverrideMethodAspect
{
    [IntroduceDependency]
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
