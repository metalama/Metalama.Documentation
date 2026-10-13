// Warning LAMA0653 on `Quantity`: `The original implementation of 'OrderLine.Equals(OrderLine?)' generated for record 'OrderLine' reads the property 'OrderLine.Quantity', whereas the C# compiler reads its backing field. An aspect overrides the property with a template that does not call the original implementation, so the property has no backing field left and the generated body reads the value that the aspect returns instead of the value that the record stores. Call 'meta.Proceed()' in the template that overrides the property.`
using System.Collections.Generic;
namespace Doc.LAMA0653.Error;
[TrackQuantity]
public record OrderLine
{
  public int Quantity
  {
    get
    {
      return 1;
    }
    set
    {
    }
  }
  public virtual bool Equals(OrderLine? other)
  {
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.Quantity, other.Quantity));
  }
}