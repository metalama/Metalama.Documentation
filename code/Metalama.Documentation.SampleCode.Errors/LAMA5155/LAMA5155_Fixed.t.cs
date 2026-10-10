using System.ComponentModel;
using Metalama.Patterns.Observability;
namespace Doc.LAMA5155.Fixed;
[Observable]
public partial class Product : INotifyPropertyChanged
{
  private decimal _price;
  public decimal Price
  {
    get
    {
      return _price;
    }
    set
    {
      if (_price != value)
      {
        _price = value;
        OnPropertyChanged("Price");
      }
    }
  }
  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  public event PropertyChangedEventHandler? PropertyChanged;
}
// The [Observable] aspect is inherited from Product.
public partial class DiscountedProduct : Product
{
  private decimal _discountedPrice;
  // Fixed: the property has its own name instead of hiding Price.
  public decimal DiscountedPrice
  {
    get
    {
      return _discountedPrice;
    }
    set
    {
      if (_discountedPrice != value)
      {
        _discountedPrice = value;
        OnPropertyChanged("DiscountedPrice");
      }
    }
  }
  protected override void OnPropertyChanged(string propertyName)
  {
    base.OnPropertyChanged(propertyName);
  }
}