// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0255.Fixed;

public class LogFirstParameterAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Fixed: the IParameter is read from the compile-time code model.
        IParameter parameter = meta.Target.Parameters[0];
        Console.WriteLine( parameter.Name );

        return meta.Proceed();
    }
}
