// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0067.Error;

// Implements a factory method by invoking a constructor of the declaring type.
public class FactoryMethodAttribute : MethodAspect
{
    public override void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        // Error: the static constructor can't be used to create an instance.
        builder.Override(
            nameof(Template),
            new { constructor = builder.Target.DeclaringType.StaticConstructor } );
    }

    [Template]
    public dynamic? Template( [CompileTime] IConstructor constructor )
    {
        return constructor.Invoke();
    }
}
