// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.LAMA0101.Error;

public class LogEntryAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: LINQ query syntax isn't supported in a template.
        var parameterNames = from parameter in meta.Target.Parameters
                             select parameter.Name;

        Console.WriteLine( $"Entering {meta.Target.Method.Name}( {string.Join( ", ", parameterNames )} )." );

        return meta.Proceed();
    }
}
