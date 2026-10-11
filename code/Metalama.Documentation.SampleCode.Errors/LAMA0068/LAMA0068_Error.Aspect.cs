// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0068.Error;

public class LogAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
#if TESTRUNNER
        // Error: the Overide method does not exist, so the aspect contains a C# error.
        builder.Overide( nameof(this.OverrideMethod) );
#else
        builder.Override( nameof(this.OverrideMethod) );
#endif
    }

    [Template]
    private dynamic? OverrideMethod()
    {
        Console.WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}
