// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0232.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // A regular string literal is compatible with C# 10.
        var message = "Logged by the \"Log\" aspect.";
        Console.WriteLine( message );

        return meta.Proceed();
    }
}
