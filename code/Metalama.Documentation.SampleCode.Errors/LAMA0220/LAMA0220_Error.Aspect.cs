// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0220.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Error: 'EntryMessage' is a template property, but a template can only call template methods.
        Console.WriteLine( EntryMessage );

        return meta.Proceed();
    }

    [Template]
    private string EntryMessage => $"Entering {meta.Target.Method}.";
}
