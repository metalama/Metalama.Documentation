// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0253.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: IMethod is a compile-time type, but AuditLog.Write is a run-time method.
        AuditLog.Write( meta.Target.Method );

        return meta.Proceed();
    }
}
