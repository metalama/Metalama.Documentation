// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0119.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( LogFormatter.GetEntryMessage( meta.Target.Method.Name ) );

        return meta.Proceed();
    }
}
