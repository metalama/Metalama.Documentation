// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0256.Error;

public class LogPropertiesAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var parameter = meta.Target.Parameters[0];

        foreach ( var property in ((INamedType) parameter.Type).Properties )
        {
            // Error: the dynamic expression is passed to the compile-time method WithObject.
            var value = property.WithObject( parameter.Value ).Value;
            Console.WriteLine( $"{property.Name} = {value}" );
        }

        return meta.Proceed();
    }
}
