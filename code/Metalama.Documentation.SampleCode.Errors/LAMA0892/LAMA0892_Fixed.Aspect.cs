// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Utilities;
using System;

namespace Doc.LAMA0892.Fixed;

// Fixed: LogFormat holds only a string, so it's marked [Durable].
[CompileTime]
[ImmutableType]
[Durable]
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
    private static readonly LogFormat _defaultFormat = new( "Entering" );

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"{_defaultFormat.Prefix} {meta.Target.Method}." );

        return meta.Proceed();
    }
}
