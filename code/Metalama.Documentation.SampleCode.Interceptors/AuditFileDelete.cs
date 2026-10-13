// This is public domain Metalama sample code.

using System.IO;

namespace Doc.AuditFileDelete;

public class ReportCleaner
{
    public void DeleteReport( string directory, string name )
        => File.Delete( Path.Combine( directory, name + ".pdf" ) );

    public void DeleteDraft( string directory, string name )
        => File.Delete( Path.Combine( directory, name + ".draft" ) );
}
