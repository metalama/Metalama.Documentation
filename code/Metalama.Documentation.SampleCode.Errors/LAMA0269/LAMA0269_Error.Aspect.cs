// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0269.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // This anonymous object is created at run time, in the target code.
        var entry = new { Name = "Calculator" };
        Console.WriteLine( entry );

        // Error: the same anonymous type, with a single string property named Name, is created at compile time.
        var target = meta.CompileTime( new { Name = meta.Target.Method.Name } );

        return meta.Proceed();
    }
}
