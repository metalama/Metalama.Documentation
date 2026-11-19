// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0887.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the template reads the method name from meta.Target instead of an aspect field.
        Console.WriteLine( $"Entering {meta.Target.Method.Name}." );

        return meta.Proceed();
    }
}
