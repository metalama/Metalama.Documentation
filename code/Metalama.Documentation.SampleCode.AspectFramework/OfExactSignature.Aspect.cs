// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;
using System.Linq;

namespace Doc.OfExactSignature;

public class WrapWithValidationAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Find a Validate method with the same parameter types as the target method.
        var parameterTypes = meta.Target.Method.Parameters.Select( p => p.Type ).ToList();

        var validateMethod = meta.Target.Type.Methods.OfExactSignature(
            "Validate",
            parameterTypes );

        if ( validateMethod != null )
        {
            // Call the validation method with the same arguments.
            validateMethod.Invoke( meta.Target.Parameters.ToValueArray() );
        }

        return meta.Proceed();
    }
}
