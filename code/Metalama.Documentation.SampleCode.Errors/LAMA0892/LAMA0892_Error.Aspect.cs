// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Utilities;
using System;

namespace Doc.LAMA0892.Error;

// An immutable compile-time type, which isn't marked [Durable].
[CompileTime]
[ImmutableType]
public sealed class LogFormat
{
    public LogFormat( string prefix )
    {
        this.Prefix = prefix;
    }

    public string Prefix { get; }
}

public class LogAttribute : OverrideMethodAspect
{
    // Warning: the static field outlives every compilation, but LogFormat isn't known to be durable.
    private static readonly LogFormat _defaultFormat = new( "Entering" );

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{_defaultFormat.Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
