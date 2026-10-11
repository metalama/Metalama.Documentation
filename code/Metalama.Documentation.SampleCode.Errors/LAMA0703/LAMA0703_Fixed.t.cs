using Metalama.Framework.RunTime;
namespace Doc.LAMA0703.Fixed;
public interface IAuditSink
{
  void Write(string message);
}
// Fixed: the aspect is applied to a type.
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