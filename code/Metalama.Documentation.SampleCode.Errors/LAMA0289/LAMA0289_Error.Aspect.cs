// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0289.Error;

public class LogTokenAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: '?.' is applied to a compile-time parameter to access its run-time value.
        var normalizedToken = meta.Target.Parameters.FirstOrDefault( p => p.Name == "token" )?.Value?.ToLower();

        return meta.Proceed();
    }
}
