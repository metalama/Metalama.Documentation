// This is public domain Metalama sample code.

using System.IO;

namespace Doc.AuditFileDelete;

internal class Program
{
    private static void Main()
    {
        var cleaner = new ReportCleaner();
        cleaner.DeleteReport( Path.GetTempPath(), "sales-2026" );
        cleaner.DeleteDraft( Path.GetTempPath(), "sales-2027" );
    }
}
