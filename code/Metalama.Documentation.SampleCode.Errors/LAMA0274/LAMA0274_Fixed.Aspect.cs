// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0274.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        LoggingTemplates.LogEntry();

        return meta.Proceed();
    }
}

// Fixed: the class implements ITemplateProvider. It can't be static, so it has a private constructor instead.
internal class LoggingTemplates : ITemplateProvider
{
    private LoggingTemplates() { }

    [Template]
    public static void LogEntry()
    {
        Console.WriteLine( $"Entering {meta.Target.Method}." );
    }
}
