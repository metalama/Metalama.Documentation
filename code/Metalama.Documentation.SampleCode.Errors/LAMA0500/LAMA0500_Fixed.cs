// This is public domain Metalama sample code.

using System.Collections.Generic;

namespace Doc.LAMA0500.Fixed;

// Fixed: the aspect overrides the existing Reset method.
[Resettable]
internal partial class ShoppingCart
{
    private readonly List<string> _items = new();

    public void Add( string item ) => this._items.Add( item );

    public void Reset() => this._items.Clear();
}
