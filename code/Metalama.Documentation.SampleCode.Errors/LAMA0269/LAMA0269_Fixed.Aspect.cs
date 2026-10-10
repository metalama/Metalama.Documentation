// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0269.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // This anonymous object is created at run time, in the target code.
        var entry = new { Name = "Calculator" };
        Console.WriteLine( entry );

        // Fixed: the compile-time anonymous object has a different property name, so it has a different anonymous type.
        var target = meta.CompileTime( new { MethodName = meta.Target.Method.Name } );

        return meta.Proceed();
    }
}
