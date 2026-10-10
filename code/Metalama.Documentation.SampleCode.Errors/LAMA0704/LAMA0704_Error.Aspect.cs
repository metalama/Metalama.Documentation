// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0704.Error;

public class AuditAttribute : TypeAspect
{
    [IntroduceDependency]
    private readonly IAuditSink _auditSink;
}
