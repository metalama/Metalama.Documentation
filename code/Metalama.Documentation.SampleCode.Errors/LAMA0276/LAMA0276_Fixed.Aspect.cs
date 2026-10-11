// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.LAMA0276.Fixed;

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        LogResultType<int>();

        return meta.Proceed();
    }

    // Fixed: the type parameter T is marked as compile-time.
    [Template]
    private void LogResultType<[CompileTime] T>()
    {
        Console.WriteLine( $"{meta.Target.Method.Name} returns {typeof( T ).Name}." );
    }
}
