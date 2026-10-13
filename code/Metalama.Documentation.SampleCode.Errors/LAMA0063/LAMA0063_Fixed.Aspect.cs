// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.Invokers;
using System.Linq;

namespace Doc.LAMA0063.Fixed;

public class ValidateFirstAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        var parameterType = (INamedType) builder.Target.Parameters[0].Type;
        var validateMethod = parameterType.Methods.OfName( "Validate" ).Single();

        builder.Override( nameof(this.Template), new { validateMethod } );
    }

    [Template]
    public dynamic? Template( [CompileTime] IMethod validateMethod )
    {
        // Fixed: InvokerOptions.Default calls the final implementation of Validate.
        validateMethod.WithObject( meta.Target.Method.Parameters[0] ).WithOptions( InvokerOptions.Default ).Invoke();

        return meta.Proceed();
    }
}
