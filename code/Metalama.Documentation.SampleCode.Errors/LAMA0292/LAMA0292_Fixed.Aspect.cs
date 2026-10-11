// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0292.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {LogFormatter.Format( meta.Target.Type.Name, meta.Target.Method.Name )}." );

        return meta.Proceed();
    }
}

// Fixed: the [CompileTime] attribute is on the type, so the whole type is compile-time.
[CompileTime]
internal static class LogFormatter
{
    public static string Format( string typeName, string methodName ) => $"{typeName}::{methodName}";
}
