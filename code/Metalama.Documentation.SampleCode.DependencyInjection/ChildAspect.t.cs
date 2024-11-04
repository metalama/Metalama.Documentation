namespace Doc.ChildAspect;
[AuditedObject]
public class Invoice
{
  public void SomeAuditedOperation()
  {
    _auditSink.Audit(this, "SomeAuditedOperation", string.Join(", ", new object[] { }));
  }
  [AuditedMember(false)]
  public void SomeNonAuditedOperation()
  {
  }
  private string? _someAuditedProperty;
  public string? SomeAuditedProperty
  {
    get
    {
      return _someAuditedProperty;
    }
    set
    {
      _auditSink.Audit(this, "set_SomeAuditedProperty", string.Join(", ", new object[] { value }));
      _someAuditedProperty = value;
    }
  }
  private IAuditSink _auditSink;
  public Invoice(IAuditSink? auditSink = null)
  {
    this._auditSink = auditSink ?? throw new System.ArgumentNullException(nameof(auditSink));
  }
}