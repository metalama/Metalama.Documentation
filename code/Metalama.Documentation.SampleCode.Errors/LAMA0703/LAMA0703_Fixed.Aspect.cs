// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0703.Fixed;

// A type aspect introduces the dependency into its target type.
public class AuditAttribute : TypeAspect
{
    [IntroduceDependency]
    private readonly IAuditSink _auditSink;
}
