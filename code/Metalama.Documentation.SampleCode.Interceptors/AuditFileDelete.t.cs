using System.IO;
namespace Doc.AuditFileDelete;
public class ReportCleaner
{
  public void DeleteReport(string directory, string name) => Audit.DeleteFile(path: Path.Combine(directory, name + ".pdf"), category: "FileSystem", caller: "DeleteReport", line: 10);
  public void DeleteDraft(string directory, string name) => Audit.DeleteFile(path: Path.Combine(directory, name + ".draft"), category: "FileSystem", caller: "DeleteDraft", line: 13);
}