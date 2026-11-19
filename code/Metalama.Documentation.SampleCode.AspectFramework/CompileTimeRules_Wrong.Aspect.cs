// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;

namespace Doc.CompileTimeRules_Wrong;

public sealed class LogAttribute : OverrideMethodAspect
{
    // WRONG. The same aspect instance can be reused for several targets and, at design time,
    // for several compilations, so this counter does not count what you expect.
    private int _targetCount;

    // WRONG. A static field of compile-time code is shared by every aspect instance, every
    // target and every compilation, and it lives as long as the IDE process.
    private static readonly Dictionary<string, string> _displayNames = new();

    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        this._targetCount++;
        _displayNames[builder.Target.Name] = builder.Target.ToDisplayString();

        base.BuildAspect( builder );
    }

    public override dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
