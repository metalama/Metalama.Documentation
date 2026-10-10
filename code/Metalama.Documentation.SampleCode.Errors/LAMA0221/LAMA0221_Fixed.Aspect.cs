// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0221.Fixed;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: meta.This represents the target object.
        AuditLog.Record( meta.This, meta.Target.Method.Name );

        return meta.Proceed();
    }
}
