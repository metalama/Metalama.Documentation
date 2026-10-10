// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0071.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var writeAudit = meta.Target.Type.Methods.OfName( "WriteAudit" ).Single();

        // Error: the type argument of WriteAudit<TCategory> can't be inferred from a string argument.
        writeAudit.Invoke( meta.Target.Method.Name );

        return meta.Proceed();
    }
}
