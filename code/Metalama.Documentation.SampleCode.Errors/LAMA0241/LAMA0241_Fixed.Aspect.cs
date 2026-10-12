// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.Linq;

namespace Doc.LAMA0241.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var loggerProperty = meta.Target.Type.Properties.OfName( "Logger" ).SingleOrDefault();

        // Fixed: the compile-time test is in a compile-time if statement, and the run-time test is inside it.
        if ( loggerProperty != null )
        {
            bool isLoggerAvailable = loggerProperty.Value != null;

            if ( isLoggerAvailable )
            {
                loggerProperty.Value!.WriteLine( $"Executing {meta.Target.Method}." );
            }
        }

        return meta.Proceed();
    }
}
