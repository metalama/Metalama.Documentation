// This is public domain Metalama sample code.

namespace Doc.ChildAspect;

public interface IAuditSink
{
    void Audit( object obj, string operation, string description );
}