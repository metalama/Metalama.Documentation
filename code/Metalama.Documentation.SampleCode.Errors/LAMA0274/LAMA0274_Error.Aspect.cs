// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0274.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        LoggingTemplates.LogEntry();

        return meta.Proceed();
    }
}

[RunTimeOrCompileTime]
internal static class LoggingTemplates
{
    // Error: LoggingTemplates is not an aspect, a fabric, or an ITemplateProvider.
    [Template]
    public static void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );
    }
}
