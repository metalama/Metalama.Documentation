// This is public domain Metalama sample code.

#if TEST_OPTIONS
// The program does not run until metalama/Metalama#2235 is fixed: [ExcludeInterceptors] has no effect.
// @DisableExecuteProgram
#endif

using System.IO;

namespace Doc.AuditFileDelete;

public class ReportCleaner
{
    public void DeleteReport( string directory, string name )
        => File.Delete( Path.Combine( directory, name + ".pdf" ) );

    public void DeleteDraft( string directory, string name )
        => File.Delete( Path.Combine( directory, name + ".draft" ) );
}
