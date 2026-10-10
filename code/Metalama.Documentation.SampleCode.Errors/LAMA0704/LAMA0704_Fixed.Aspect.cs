// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0704.Fixed;

public class AuditAttribute : TypeAspect
{
    [IntroduceDependency]
    private readonly IAuditSink _auditSink;
}
