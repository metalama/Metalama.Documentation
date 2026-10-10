// This is public domain Metalama sample code.

using Metalama.Framework.RunTime.Initialization;

namespace Doc.LAMA0551.Fixed;

public class Entity
{
    // Fixed: the constructor accepts an InitializationContext and calls OnConstructed
    // only if a derived type doesn't handle it.
    public Entity( InitializationContext context = default )
    {
        if ( !context.IsHandled( InitializationSlot.OnConstructed ) )
        {
            this.OnConstructed( context );
        }
    }

    public virtual void OnConstructed( InitializationContext context = default ) { }
}

[LogConstructed]
public partial class Order : Entity
{
    public Order() { }
}
