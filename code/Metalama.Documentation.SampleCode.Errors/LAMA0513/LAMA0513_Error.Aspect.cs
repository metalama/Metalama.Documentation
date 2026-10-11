// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace Doc.LAMA0513.Error;

// Marker interface that identifies entity types in the repository.
public interface IEntity<T> { }

public class EntityAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // The type argument of IEntity<T> isn't specified.
        builder.ImplementInterface( typeof(IEntity<>) );
    }
}
