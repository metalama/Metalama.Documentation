// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0241.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var loggerProperty = meta.Target.Type.Properties.OfName( "Logger" ).SingleOrDefault();

        // Error: the left operand is compile-time, but the right operand is run-time.
        bool isLoggerAvailable = loggerProperty != null && loggerProperty.Value != null;

        if ( isLoggerAvailable )
        {
            loggerProperty!.Value!.WriteLine( $"Executing {meta.Target.Method}." );
        }

        return meta.Proceed();
    }
}
