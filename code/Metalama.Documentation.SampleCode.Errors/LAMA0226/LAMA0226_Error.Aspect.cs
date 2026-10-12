// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Collections.Generic;

namespace Doc.LAMA0226.Error;

public class AuditAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: the type combines the compile-time IParameter and the run-time AuditEntry.
        Dictionary<IParameter, AuditEntry> entries = new();

        return meta.Proceed();
    }
}
