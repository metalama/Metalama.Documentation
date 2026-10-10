// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;

namespace Doc.LAMA0253.Fixed;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the name of the method is a string, which can be passed to run-time code.
        AuditLog.Write( meta.Target.Method.Name );

        return meta.Proceed();
    }
}
