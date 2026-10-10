// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0012.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Finds the WriteAudit method of the target type.
        var writeAudit = meta.Target.Type.Methods.OfName( "WriteAudit" ).Single();

        // Error: WriteAudit has two parameters, but only one argument is passed.
        writeAudit.Invoke( meta.Target.Method.Name );

        return meta.Proceed();
    }
}
