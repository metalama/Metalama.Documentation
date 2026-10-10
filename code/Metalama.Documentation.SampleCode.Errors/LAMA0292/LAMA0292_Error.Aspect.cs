// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0292.Error;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {LogFormatter.Format( meta.Target.Type.Name, meta.Target.Method.Name )}." );

        return meta.Proceed();
    }
}

// This type is not marked [CompileTime], so it is run-time.
internal static class LogFormatter
{
    // Error: a compile-time method cannot be declared in a run-time type.
    [CompileTime]
    public static string Format( string typeName, string methodName ) => $"{typeName}::{methodName}";
}
