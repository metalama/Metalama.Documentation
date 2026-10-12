// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Collections.Generic;

namespace Doc.LAMA0226.Fixed;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the dictionary is keyed by the parameter name, so all its type arguments are run-time.
        var entries = new Dictionary<string, AuditEntry>();

        foreach ( var parameter in meta.Target.Parameters )
        {
            entries.Add( parameter.Name, new AuditEntry( parameter.Value ) );
        }

        AuditLog.Write( meta.Target.Method.Name, entries );

        return meta.Proceed();
    }
}
