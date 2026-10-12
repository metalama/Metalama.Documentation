// This is public domain Metalama sample code.

using Metalama.Framework.RunTime.Initialization;

namespace Doc.LAMA0551.Error;

public class Entity
{
    // Error: Entity defines OnConstructed but no constructor accepting an InitializationContext.
    public Entity() { }

    public virtual void OnConstructed( InitializationContext context = default ) { }
}

[LogConstructed]
public partial class Order : Entity
{
    public Order() { }
}
