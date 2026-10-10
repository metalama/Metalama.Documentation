// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.LAMA0227.Fixed;

public class LogArgumentsAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the list stores the argument values as object.
        var values = new List<object?>( meta.Target.Parameters.ToValueArray() );

        Console.WriteLine( $"{meta.Target.Method.Name}({string.Join( ", ", values )})" );

        return meta.Proceed();
    }
}
