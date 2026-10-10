// This is public domain Metalama sample code.

using System.Collections.Generic;

namespace Doc.LAMA0502.Fixed;

internal class Cart
{
    protected readonly List<string> Items = new();

    // Fixed: the method is virtual, so the aspect can override it in ShoppingCart.
    public virtual void Reset() => this.Items.Clear();
}

[Resettable]
internal partial class ShoppingCart : Cart
{
    public void Add( string item ) => this.Items.Add( item );
}
