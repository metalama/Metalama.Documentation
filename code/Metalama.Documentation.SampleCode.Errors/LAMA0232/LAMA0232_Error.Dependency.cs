// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0232.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Raw string literals require C# 11, but the target project uses C# 10.
        var message = """Logged by the "Log" aspect.""";
        Console.WriteLine( message );

        return meta.Proceed();
    }
}
