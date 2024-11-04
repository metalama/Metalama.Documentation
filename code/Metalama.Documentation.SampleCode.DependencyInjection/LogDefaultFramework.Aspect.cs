// This is public domain Metalama sample code.

using Doc.LogDefaultFramework;
using Metalama.Framework.Aspects;
using Metalama.Extensions.DependencyInjection;

[assembly:
    AspectOrder( AspectOrderDirection.RunTime, typeof(LogAttribute), typeof(DependencyAttribute) )]

namespace Doc.LogDefaultFramework;

// Our logging aspect.
public class LogAttribute : OverrideMethodAspect
{
    // Defines the dependency consumed by the aspect. It will be handled by the dependency injection framework configured for the current project.
    // By default, this is the .NET Core system one, which pulls dependencies from the constructor.
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