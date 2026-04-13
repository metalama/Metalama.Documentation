// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.OfExactSignature;

public class WrapWithValidationAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        // Find a Validate method that takes (int, int) using System.Type overload.
        var validateMethod = meta.Target.Type.Methods.OfExactSignature(
            "Validate",
            [typeof(int), typeof(int)] );

        if ( validateMethod != null )
        {
            // Call the validation method with compile-time foreach to forward arguments.
            validateMethod.Invoke( meta.Target.Parameters[0].Value, meta.Target.Parameters[1].Value );
        }

        return meta.Proceed();
    }
}
