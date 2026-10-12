// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Aspects;

namespace Doc.LAMA0900.Error;

[Experimental]
public static class PdfExporter
{
    public static void Export( string path ) { }
}

public class ReportService
{
    public void Save()
    {
        // Warning: PdfExporter is experimental, but Save is not.
        PdfExporter.Export( "report.pdf" );
    }
}
