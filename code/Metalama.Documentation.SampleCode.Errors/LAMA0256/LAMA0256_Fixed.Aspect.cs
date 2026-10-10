// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0256.Fixed;

public class LogPropertiesAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var parameter = meta.Target.Parameters[0];

        foreach ( var property in ((INamedType) parameter.Type).Properties )
        {
            // Fixed: the dynamic expression is explicitly cast to IExpression.
            var value = property.WithObject( (IExpression) parameter.Value! ).Value;
            Console.WriteLine( $"{property.Name} = {value}" );
        }

        return meta.Proceed();
    }
}
