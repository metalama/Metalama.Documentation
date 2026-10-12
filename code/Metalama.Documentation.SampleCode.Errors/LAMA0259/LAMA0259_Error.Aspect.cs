// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0259.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var logField = meta.Target.Type.Fields.OfName( "_log" ).SingleOrDefault();

        // Error: logField is compile-time, but logField.Value is run-time.
        var log = logField?.Value;
        log?.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
