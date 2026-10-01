// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Microsoft.Extensions.Logging;

namespace Doc.LoggerParameterReuse;

// A logging aspect that introduces an ILogger dependency.
// The default DI framework resolves ILogger as ILogger<T> where T is the target type.
public class LogAttribute : OverrideMethodAspect
{
    [IntroduceDependency]
    private readonly ILogger _logger;

    public override dynamic? OverrideMethod()
    {
        this._logger.LogWarning( $"{meta.Target.Method} called." );

        return meta.Proceed();
    }
}
