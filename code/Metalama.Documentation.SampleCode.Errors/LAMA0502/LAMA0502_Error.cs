// This is public domain Metalama sample code.

using System.Collections.Generic;

namespace Doc.LAMA0502.Error;

internal class Cart
{
    protected readonly List<string> Items = new();

    // Error: the method is not virtual, so the aspect cannot override it in ShoppingCart.
    public void Reset() => this.Items.Clear();
}

[Resettable]
internal partial class ShoppingCart : Cart
{
    public void Add( string item ) => this.Items.Add( item );
}
