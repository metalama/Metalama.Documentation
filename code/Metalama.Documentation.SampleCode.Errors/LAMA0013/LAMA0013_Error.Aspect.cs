// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0013.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Finds the WriteAudit method of the target type.
        var writeAudit = meta.Target.Type.Methods.OfName( "WriteAudit" ).Single();

        // Error: WriteAudit requires at least the methodName argument, but no argument is passed.
        writeAudit.Invoke();

        return meta.Proceed();
    }
}
