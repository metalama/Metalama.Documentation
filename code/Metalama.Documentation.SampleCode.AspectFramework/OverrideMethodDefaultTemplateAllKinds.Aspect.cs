// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.OverrideMethodDefaultTemplateAllKinds;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{meta.Target.Method.Name}: start" );
        var result = meta.Proceed();
        Console.WriteLine( $"{meta.Target.Method.Name}: returning {result}." );

        return result;
    }
}