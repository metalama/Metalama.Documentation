// Warning LAMA5022 on `Items`: `The type of the 'Order.Items' property must be deeply immutable.`
using Metalama.Patterns.Immutability;
using System.Collections.Generic;
namespace Doc.LAMA5022.Error;
[Immutable(ImmutabilityKind.Deep)]
public class Order
{
  public string Customer { get; }
  // Warning: List<T> is a mutable type, so the property isn't deeply immutable.
  public List<string> Items { get; }
  public Order(string customer, IEnumerable<string> items)
  {
    this.Customer = customer;
    this.Items = new List<string>(items);
  }
}