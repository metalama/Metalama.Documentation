// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0285.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        LogEntry();

        return meta.Proceed();

        // Error: a local function can't be marked as a template.
        [Template]
        void LogEntry()
        {
            Console.WriteLine( $"Entering {meta.Target.Method.Name}." );
        }
    }
}
