// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0221.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: in a template, 'this' is the aspect instance, not the target object.
        AuditLog.Record( this, meta.Target.Method.Name );

        return meta.Proceed();
    }
}
