// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace Doc.LAMA0703.Error;

// A compilation aspect has no type into which the dependency could be introduced.
public class AuditAttribute : CompilationAspect
{
    [IntroduceDependency]
    private readonly IAuditSink _auditSink;
}
