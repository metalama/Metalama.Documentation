// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.LAMA0101.Fixed;

public class LogEntryAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the LINQ method syntax is supported in a template.
        var parameterNames = meta.Target.Parameters.Select( parameter => parameter.Name );

        Console.WriteLine( $"Entering {meta.Target.Method.Name}( {string.Join( ", ", parameterNames )} )." );

        return meta.Proceed();
    }
}
