// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0067.Fixed;

// Implements a factory method by invoking a constructor of the declaring type.
public class FactoryMethodAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Fixed: the parameterless instance constructor is used.
        builder.Override(
            nameof(Template),
            new { constructor = builder.Target.DeclaringType.Constructors.Single( c => c.Parameters.Count == 0 ) } );
    }

    [Template]
    public dynamic? Template( [CompileTime] IConstructor constructor )
    {
        return constructor.Invoke();
    }
}
