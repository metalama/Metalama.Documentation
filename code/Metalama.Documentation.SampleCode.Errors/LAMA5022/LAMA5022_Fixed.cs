// This is public domain Metalama sample code.

using Metalama.Patterns.Immutability;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Doc.LAMA5022.Fixed;

[Immutable( ImmutabilityKind.Deep )]
public class Order
{
    public string Customer { get; }

    // Fixed: ImmutableArray<string> is deeply immutable.
    public ImmutableArray<string> Items { get; }

    public Order( string customer, IEnumerable<string> items )
    {
        this.Customer = customer;
        this.Items = items.ToImmutableArray();
    }
}
