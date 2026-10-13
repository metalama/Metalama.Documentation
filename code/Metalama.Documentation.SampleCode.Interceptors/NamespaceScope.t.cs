using System.IO;
namespace Doc.NamespaceScope.Billing
{
  // The calls in this namespace are intercepted.
  public class InvoiceLoader
  {
    public string Load(string path) => AuditedFile.ReadAllText(path);
  }
}
namespace Doc.NamespaceScope.Reporting
{
  // The calls in this namespace are not intercepted.
  public class ReportLoader
  {
    public string Load(string path) => File.ReadAllText(path);
  }
}