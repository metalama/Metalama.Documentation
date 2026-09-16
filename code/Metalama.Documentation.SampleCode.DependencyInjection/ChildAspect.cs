// This is public domain Metalama sample code.

namespace Doc.ChildAspect;

[AuditedObject]
public class Invoice
{
    public void SomeAuditedOperation() { }

    [AuditedMember( false )]
    public void SomeNonAuditedOperation() { }

    public string? SomeAuditedProperty { get; set; }
}