using Metalama.Extensions.Architecture.Aspects;
namespace Doc.LAMA0900.Fixed;
[Experimental]
public static class PdfExporter
{
  public static void Export(string path)
  {
  }
}
public class ReportService
{
  // Fixed: the method that uses the experimental API is marked as experimental too.
  [Experimental]
  public void Save()
  {
    PdfExporter.Export("report.pdf");
  }
}