// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0513.Fixed;

// Marker interface that identifies entity types in the repository.
public interface IEntity<T> { }

public class EntityAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The target type is given as the type argument of IEntity<T>.
        var interfaceType = builder.Target.Compilation.Factory
            .GetNamedTypeByReflectionType( typeof(IEntity<>) )
            .MakeGenericInstance( builder.Target );

        builder.ImplementInterface( interfaceType );
    }
}
