using System;
using System.Collections.Generic;
namespace Doc.LAMA0653.Fixed;
[TrackQuantity]
public record OrderLine
{
  private int _quantity;
  public int Quantity
  {
    get
    {
      Console.WriteLine("Reading the quantity.");
      return _quantity;
    }
    set
    {
      _quantity = value;
    }
  }
  public virtual bool Equals(OrderLine? other)
  {
    return (object)this == (object? )other || ((object? )other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this._quantity, other._quantity));
  }
}