// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using System;

namespace Doc.ImportAttributeFramework;

// A logging aspect that introduces a logger dependency using our custom framework.
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
