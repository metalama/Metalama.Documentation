// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0071.Fixed;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var writeAudit = meta.Target.Type.Methods.OfName( "WriteAudit" ).Single();

        // Fixed: the type argument is supplied explicitly.
        writeAudit.MakeGenericInstance( meta.Target.Type ).Invoke( meta.Target.Method.Name );

        return meta.Proceed();
    }
}
