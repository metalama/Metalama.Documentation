// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Diagnostics;

namespace Doc.AspectConfiguration;

// The aspect itself, consuming the configuration.
public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var options = meta.Target.Method.Enhancements().GetOptions<LoggingOptions>();

        var message = $"{options.Category}: Executing {meta.Target.Method}.";

        switch ( options.Level!.Value )
        {
            case TraceLevel.Error:
                Trace.TraceError( message );

                break;

            case TraceLevel.Info:
                Trace.TraceInformation( message );

                break;

            case TraceLevel.Warning:
                Trace.TraceWarning( message );

                break;

            case TraceLevel.Verbose:
                Trace.WriteLine( message );

                break;
        }

        return meta.Proceed();
    }
}