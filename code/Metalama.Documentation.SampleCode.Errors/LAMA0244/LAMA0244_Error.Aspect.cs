// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0244.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var name = new UpperCaseNameFormatter().Format( meta.Target.Method.Name );
        Console.WriteLine( $"Executing {name}." );

        return meta.Proceed();
    }
}
