// This is public domain Metalama sample code.

using Doc.LogServiceLocator;
using Metalama.Framework.Aspects;
using Metalama.Extensions.DependencyInjection;

[assembly:
    AspectOrder( AspectOrderDirection.RunTime, typeof(LogAttribute), typeof(DependencyAttribute) )]

namespace Doc.LogServiceLocator;

// Our logging aspect.
public class LogAttribute : OverrideMethodAspect
{
    // Defines the dependency consumed by the aspect. It will be handled initialized from a service locator,
    // but note that the aspect does not need to know the implementation details of the dependency injection framework.
    [IntroduceDependency]
    private readonly IMessageWriter _messageWriter;

    public override dynamic? OverrideMethod()
    {
        try
        {
            this._messageWriter.Write( $"{meta.Target.Method} started." );

            return meta.Proceed();
        }
        finally
        {
            this._messageWriter.Write( $"{meta.Target.Method} completed." );
        }
    }
}