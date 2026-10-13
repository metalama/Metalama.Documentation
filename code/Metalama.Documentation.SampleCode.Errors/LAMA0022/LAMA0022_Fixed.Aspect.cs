// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

// The parent aspect, LogAllMethods, is now processed before its child aspect, Log.
[assembly: AspectOrder(
    AspectOrderDirection.CompileTime,
    typeof(Doc.LAMA0022.Fixed.LogAllMethodsAttribute),
    typeof(Doc.LAMA0022.Fixed.LogAttribute) )]

namespace Doc.LAMA0022.Fixed;

public class LogAllMethodsAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Adds a child aspect to each method of the type.
        builder.Outbound.SelectMany( t => t.Methods ).AddAspect( _ => new LogAttribute() );
    }
}

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
