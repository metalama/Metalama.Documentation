using Metalama.Framework.RunTime;
namespace Doc.LAMA0704.Fixed;
// Fixed: the dependency type is public, like the constructor that receives it.
public interface IAuditSink
{
  void Write(string message);
}
[Audit]
public partial class OrderService
{
  public void PlaceOrder()
  {
  }
  private IAuditSink _auditSink;
  public OrderService([AspectGenerated] IAuditSink? auditSink = null)
  {
    this._auditSink = auditSink ?? throw new System.ArgumentNullException(nameof(auditSink));
  }
}