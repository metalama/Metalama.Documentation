// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0012.Fixed;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Finds the WriteAudit method of the target type.
        var writeAudit = meta.Target.Type.Methods.OfName( "WriteAudit" ).Single();

        // Fixed: one argument is passed for each parameter of WriteAudit.
        writeAudit.Invoke( meta.Target.Type.Name, meta.Target.Method.Name );

        return meta.Proceed();
    }
}
