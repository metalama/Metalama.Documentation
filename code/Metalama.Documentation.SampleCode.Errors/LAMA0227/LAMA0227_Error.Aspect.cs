// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.LAMA0227.Error;

public class LogArgumentsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: dynamic cannot be used as a generic argument in a template.
        var values = new List<dynamic?>( meta.Target.Parameters.ToValueArray() );

        Console.WriteLine( $"{meta.Target.Method.Name}({string.Join( ", ", values )})" );

        return meta.Proceed();
    }
}
