// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0065.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var options = meta.Target.Method.DeclaringType.Enhancements().GetOptions<LoggingOptions>();

        Console.WriteLine( $"{options.Category}: Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
