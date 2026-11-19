// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0887.Error;

public class LogAttribute : OverrideMethodAspect
{
    private string? _methodName;

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Warning: the aspect field is assigned outside a constructor.
        this._methodName = builder.Target.Name;

        base.BuildAspect( builder );
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Entering {this._methodName}." );

        return meta.Proceed();
    }
}
