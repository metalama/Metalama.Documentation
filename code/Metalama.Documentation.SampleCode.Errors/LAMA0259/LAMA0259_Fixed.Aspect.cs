// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0259.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var logField = meta.Target.Type.Fields.OfName( "_log" ).SingleOrDefault();

        // Fixed: a compile-time 'if' statement checks logField for null.
        if ( logField != null )
        {
            var log = logField.Value;
            log?.WriteLine( $"Executing {meta.Target.Method}." );
        }

        return meta.Proceed();
    }
}
