// This is public domain Metalama sample code.

using System.IO;

namespace Doc.LAMA0530.Fixed;

[InjectDependencies]
internal partial class ReportGenerator
{
    // Both fields have the type TextWriter.
    [Dependency]
    private readonly TextWriter? _auditLog;

    [Dependency]
    private readonly TextWriter? _errorLog;

    public ReportGenerator() { }
}
